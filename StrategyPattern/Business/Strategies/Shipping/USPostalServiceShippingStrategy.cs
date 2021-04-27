using Strategy_Pattern_First_Look.Business.Models;
using System;
using System.Net.Http;

namespace StrategyPattern.Business.Strategies.Shipping
{
    class USPostalServiceShippingStrategy : IShippingStrategy
    {
        public void Ship(Order order)
        {
            using HttpClient client = new();
            Console.WriteLine("Order shipped with USPS");
        }
    }
}
