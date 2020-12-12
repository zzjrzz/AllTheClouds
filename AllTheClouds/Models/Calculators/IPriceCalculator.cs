using System.Collections.Generic;

namespace AllTheClouds.Models.Calculators
{
    public interface IPriceCalculator
    {
        IEnumerable<ProductsResponse> CalculatePrices(IEnumerable<ProductsResponse> products);
    }
}
