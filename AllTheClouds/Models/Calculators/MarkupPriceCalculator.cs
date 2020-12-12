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

        public IEnumerable<ProductsResponse> CalculatePrices(IEnumerable<ProductsResponse> products)
        {
            var pricedProducts = products.ToArray();
            foreach (var product in pricedProducts)
            {
                product.UnitPrice *= _markupMultiplier;
            }

            return pricedProducts;
        }
    }
}
