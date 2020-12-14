using System.Collections.Generic;
using System.Linq;

namespace AllTheClouds.Models.Calculators
{
    public class MarkupPriceCalculator : IPriceCalculator
    {
        private readonly decimal _markupMultiplier;

        public MarkupPriceCalculator(decimal markupMultiplier)
        {
            _markupMultiplier = markupMultiplier;
        }

        public IEnumerable<ProductResponse> CalculatePrices(IEnumerable<ProductResponse> products)
        {
            var productResponses = products.ToList();
            foreach (var product in productResponses)
            {
                product.UnitPrice *= _markupMultiplier;
            }

            return productResponses;
        }
    }
}
