using System.Collections.Generic;
using AllTheClouds.Models.DTO;

namespace AllTheClouds.Models.Calculators
{
    public interface IPriceCalculator
    {
        IEnumerable<ProductResponse> CalculatePrices(IEnumerable<ProductResponse> products);
    }
}
