using System.Collections.Generic;

namespace AllTheClouds.Models.Calculators
{
    public interface IPriceCalculator
    {
        IEnumerable<ProductResponse> CalculatePrices(IEnumerable<ProductResponse> products);
    }
}
