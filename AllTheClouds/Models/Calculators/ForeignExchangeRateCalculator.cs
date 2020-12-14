using System.Collections.Generic;
using System.Linq;

namespace AllTheClouds.Models.Calculators
{
    public class ForeignExchangeRateCalculator : IPriceCalculator
    {
        private readonly IEnumerable<ForeignExchangeRateResponse> _fxRates;
        private readonly string _sourceCurrency;
        private readonly string _targetCurrency;

        public ForeignExchangeRateCalculator(IEnumerable<ForeignExchangeRateResponse> fxRates, string sourceCurrency, string targetCurrency)
        {
            _fxRates = fxRates;
            _sourceCurrency = sourceCurrency;
            _targetCurrency = targetCurrency;
        }

        public IEnumerable<ProductResponse> CalculatePrices(IEnumerable<ProductResponse> products)
        {
            var pricedProducts = products.ToArray();
            var fxRate = _fxRates.Single(rate => rate.TargetCurrency == _targetCurrency && rate.SourceCurrency == _sourceCurrency);

            foreach (var product in pricedProducts)
            {
                product.UnitPrice *= fxRate.Rate;
            }

            return pricedProducts;
        }
    }
}
