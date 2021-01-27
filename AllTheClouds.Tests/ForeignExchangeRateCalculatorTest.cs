using System.Collections.Generic;
using System.Linq;
using AllTheClouds.Models;
using AllTheClouds.Models.Calculators;
using AllTheClouds.Models.DTO;
using Xunit;

namespace AllTheClouds.Tests
{
    public class ForeignExchangeRateCalculatorTest
    {
        [Fact]
        public void Given_The_Fx_Rates_All_Product_Prices_Are_Increased_According_To_The_Source_And_Target_Currency()
        {
            var fxRates = new List<ForeignExchangeRateResponse>
            {
                new ForeignExchangeRateResponse {Rate = 0.5m, SourceCurrency = "AUD", TargetCurrency = "GBP"},
                new ForeignExchangeRateResponse {Rate = 2m, SourceCurrency = "AUD", TargetCurrency = "USD"}
            };
            var products = new List<ProductResponse> { new ProductResponse { UnitPrice = 1 }, new ProductResponse { UnitPrice = 2 } };
            var priceCalculator = new PriceCalculator(new ForeignExchangeRateCalculator(fxRates, Currency.AUD, Currency.USD));
            var fxRatedProducts = priceCalculator.Calculate(products).ToArray();

            Assert.Equal( 2m, fxRatedProducts[0].UnitPrice);
            Assert.Equal(4m, fxRatedProducts[1].UnitPrice);
        }
    }
}