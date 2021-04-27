using Strategy_Pattern_First_Look.Business.Models;
using StrategyPattern.Business.Strategies.Invoice;
using StrategyPattern.Business.Strategies.SalesTax;
using StrategyPattern.Business.Strategies.Shipping;
using System;

namespace Strategy_Pattern_First_Look
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            #region Input

            Console.WriteLine("Please select an origin country: ");
            var origin = Console.ReadLine().Trim();

            Console.WriteLine("Please select a destination country: ");
            var destination = Console.ReadLine().Trim();

            Console.WriteLine("Choose one of the following shipping providers.");
            Console.WriteLine("1. DHL");
            Console.WriteLine("2. USPS");
            Console.WriteLine("3. Fedex");
            Console.WriteLine("4. UPS");
            Console.WriteLine("Select shipping provider: ");
            var provider = Convert.ToInt32(Console.ReadLine().Trim());

            Console.WriteLine("Choose one of the following invoice delivery options.");
            Console.WriteLine("1. E-mail");
            Console.WriteLine("2. File (download later)");
            Console.WriteLine("3. Mail");
            Console.WriteLine("Select invoice delivery options: ");
            var invoiceOption = Convert.ToInt32(Console.ReadLine().Trim());

            #endregion

            var order = new Order
            {
                ShippingDetails = new ShippingDetails
                {
                    OriginCountry = origin,
                    DestinationCountry = destination
                },
                SalesTaxStrategy = GetSalesTaxStrategyFor(origin),
                InvoiceStrategy = GetInvoiceStrategyFor(invoiceOption),
                ShippingStrategy = GetShippingStrategyFor(provider)
            };

            order.SelectedPayments.Add(new Payment { PaymentProvider = PaymentProvider.Invoice });

            order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m, ItemType.Literature), 1);

            Console.WriteLine(order.GetTax());

            order.FinalizeOrder();
        }

        private static IInvoiceStrategy GetInvoiceStrategyFor(int option)
        {
            return option switch
            {
                1 => new EmailInvoiceStrategy(),
                2 => new FileInvoiceStrategy(),
                3 => new PrintOnDemandInvoiceStrategy(),
                _ => throw new Exception("Unsupported invoice delivery option"),
            };
        }

        private static IShippingStrategy GetShippingStrategyFor(int provider)
        {
            return provider switch
            {
                1 => new USPostalServiceShippingStrategy(),
                2 => new DHLShippingStrategy(),
                3 => new FedexShippingStrategy(),
                4 => new UPSShippingStrategy(),
                _ => throw new Exception("Unsupported shipping method"),
            };
        }

        private static ISalesTaxStrategy GetSalesTaxStrategyFor(string origin)
        {
            if (origin.ToLowerInvariant() == "sweden")
            {
                return new SwedenSalesTaxStrategy();
            }
            else if (origin.ToLowerInvariant() == "usa")
            {
                return new USSalesTaxStrategy();
            }
            else
            {
                throw new Exception("Unsupported shipping region");
            }
        }
    }
}
