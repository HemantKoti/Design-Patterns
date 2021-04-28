using System;
using CommandPattern.Models;
using CommandPattern.Repository;

namespace CommandPattern.Commands
{
    public class ChangeQuantityCommand : ICommand
    {
        private readonly Operation operation;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;
        private readonly Product product;


        public enum Operation
        {
            Increase,
            Decrease
        }

        public ChangeQuantityCommand(Operation operation, IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, Product product)
        {
            this.operation = operation;
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
            this.product = product;
        }

        public bool CanExecute()
        {
            return this.operation switch
            {
                Operation.Increase => this.productRepository.GetStockFor(this.product.ArticleId) - 1 >= 0,
                Operation.Decrease => this.shoppingCartRepository.Get(this.product.ArticleId).Quantity != 0,
                _ => false,
            };
        }

        public void Execute()
        {
            switch (this.operation)
            {
                case Operation.Increase:
                    this.productRepository.IncreaseStockBy(this.product.ArticleId, 1);
                    this.shoppingCartRepository.DecreaseQuantity(this.product.ArticleId);
                    break;
                case Operation.Decrease:
                    this.productRepository.DecreaseStockBy(this.product.ArticleId, 1);
                    this.shoppingCartRepository.IncreaseQuantity(this.product.ArticleId);
                    break;
            }
        }

        public void Undo()
        {
            switch (operation)
            {
                case Operation.Decrease:
                    productRepository.DecreaseStockBy(product.ArticleId, 1);
                    shoppingCartRepository.IncreaseQuantity(product.ArticleId);
                    break;
                case Operation.Increase:
                    productRepository.IncreaseStockBy(product.ArticleId, 1);
                    shoppingCartRepository.DecreaseQuantity(product.ArticleId);
                    break;
            }
        }
    }
}
