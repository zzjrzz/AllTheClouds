using System.Collections.Generic;
using System.Linq;
using AllTheClouds.Models;
using AllTheClouds.Models.Calculators;
using AllTheClouds.Models.DTO;
using Xunit;

namespace AllTheClouds.Tests
{
    public class MarkupCalculatorTest
    {
        [Fact]
        public void Given_All_Products_When_Price_Calculated_Return_20_Percent_Higher()
        {
            var products = new List<ProductResponse>
                {new ProductResponse {UnitPrice = 1}, new ProductResponse {UnitPrice = 2}};
            var calculator = new PriceCalculator(new MarkupPriceCalculator(1.2m));
            var markedUpProducts = calculator.Calculate(products).ToArray();

            Assert.Equal(1.2m, markedUpProducts[0].UnitPrice);
            Assert.Equal(2.4m, markedUpProducts[1].UnitPrice);
        }
    }
}