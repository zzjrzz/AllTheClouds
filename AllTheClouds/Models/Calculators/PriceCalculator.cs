using System.Collections.Generic;
using AllTheClouds.Models.DTO;

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
