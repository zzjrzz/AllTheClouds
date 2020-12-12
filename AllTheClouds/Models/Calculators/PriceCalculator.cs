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

        public IEnumerable<ProductsResponse> Calculate(IEnumerable<ProductsResponse> products) => _priceCalculator.CalculatePrices(products);
    }
}
