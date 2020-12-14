using System.Collections.Generic;

namespace AllTheClouds.Models.Calculators
{
    public class PriceCalculator
    {
        private readonly IPriceCalculator _priceCalculator;

        public PriceCalculator(IPriceCalculator priceCalculator)
        {
            _priceCalculator = priceCalculator;
        }

        public IEnumerable<ProductResponse> Calculate(IEnumerable<ProductResponse> products) => _priceCalculator.CalculatePrices(products);
    }
}
