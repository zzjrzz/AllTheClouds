using System.Collections.Generic;
using AllTheClouds.Models;
using AllTheClouds.Models.Calculators;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Services
{
    public static class CalculatorService
    {
        public static IEnumerable<ProductResponse> MarkUpPrices(this IEnumerable<ProductResponse> products,
            decimal markupMultiplier)
        {
            var markupCalculator = new PriceCalculator(new MarkupPriceCalculator(markupMultiplier));
            return markupCalculator.Calculate(products);
        }

        public static IEnumerable<ProductResponse> ConvertCurrency(this IEnumerable<ProductResponse> products,
            IEnumerable<ForeignExchangeRateResponse> foreignExchangeRates,
            Currency sourceCurrency,
            Currency targetCurrency)
        {
            if (sourceCurrency.Equals(targetCurrency))
                return products;

            var calculator =
                new PriceCalculator(
                    new ForeignExchangeRateCalculator(foreignExchangeRates, sourceCurrency, targetCurrency));
            return calculator.Calculate(products);
        }
    }
}