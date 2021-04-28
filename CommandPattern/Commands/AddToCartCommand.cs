using CommandPattern.Models;
using CommandPattern.Repository;
using System;

namespace CommandPattern.Commands
{
    public class AddToCartCommand : ICommand
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;
        private readonly Product product;

        public AddToCartCommand(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, Product product)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
            this.product = product;
        }

        public bool CanExecute()
        {
            if (this.product == null)
                return false;

            return this.productRepository.GetStockFor(this.product.ArticleId) > 0;
        }

        public void Execute()
        {
            if (this.product == null)
                return;

            this.productRepository.DecreaseStockBy(this.product.ArticleId, 1);

            this.shoppingCartRepository.Add(this.product);
        }

        public void Undo()
        {
            if (this.product == null)
                return;

            var lineItem = this.shoppingCartRepository.Get(this.product.ArticleId);

            this.productRepository.IncreaseStockBy(this.product.ArticleId, lineItem.Quantity);

            this.shoppingCartRepository.RemoveAll(this.product.ArticleId);
        }
    }
}
