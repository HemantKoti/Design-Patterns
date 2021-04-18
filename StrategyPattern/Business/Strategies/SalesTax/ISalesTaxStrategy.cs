﻿
using Strategy_Pattern_First_Look.Business.Models;

namespace StrategyPattern.Business.Strategies.SalesTax
{
    public interface ISalesTaxStrategy
    {
        public decimal GetTaxFor(Order order);
    }
}
