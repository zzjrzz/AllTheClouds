using System.Collections.Generic;
using System.Linq;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Models.Calculators
{
    public class ForeignExchangeRateCalculator : IPriceCalculator
    {
        private readonly IEnumerable<ForeignExchangeRateResponse> _fxRates;
        private readonly Currency _sourceCurrency;
        private readonly Currency _targetCurrency;

        public ForeignExchangeRateCalculator(IEnumerable<ForeignExchangeRateResponse> fxRates,
            Currency sourceCurrency,
            Currency targetCurrency)
        {
            _fxRates = fxRates;
            _sourceCurrency = sourceCurrency;
            _targetCurrency = targetCurrency;
        }

        public IEnumerable<ProductResponse> CalculatePrices(IEnumerable<ProductResponse> products)
        {
            var pricedProducts = products.ToArray();
            var fxRate = _fxRates.Single(rate => rate.TargetCurrency == _targetCurrency.ToString() 
                                                 && rate.SourceCurrency == _sourceCurrency.ToString());

            foreach (var product in pricedProducts)
            {
                product.UnitPrice *= fxRate.Rate;
            }

            return pricedProducts;
        }
    }
}
