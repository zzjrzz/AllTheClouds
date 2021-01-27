using System;
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
            string sourceCurrency,
            string targetCurrency)
        {
            if (targetCurrency == null || sourceCurrency == null)
                throw new ArgumentException("targetCurrency and sourceCurrency cannot be null");

            var convertSourceCurrency = Enum.TryParse(sourceCurrency, out Currency source);
            var convertTargetCurrency = Enum.TryParse(targetCurrency, out Currency target);

            if (!convertSourceCurrency || !convertTargetCurrency)
                throw new FormatException("Unable to parse sourceCurrency or targetCurrency");

            if (sourceCurrency.Equals(targetCurrency))
                return products;

            var calculator =
                new PriceCalculator(new ForeignExchangeRateCalculator(foreignExchangeRates, source, target));
            return calculator.Calculate(products);
        }
    }
}