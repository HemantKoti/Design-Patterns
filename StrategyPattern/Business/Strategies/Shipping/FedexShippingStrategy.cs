using Strategy_Pattern_First_Look.Business.Models;
using System;
using System.Net.Http;

namespace StrategyPattern.Business.Strategies.Shipping
{
    class FedexShippingStrategy : IShippingStrategy
    {
        public void Ship(Order order)
        {
            using HttpClient httpClient = new();
            Console.WriteLine("Order shipped with Fedex");
        }
    }
}
