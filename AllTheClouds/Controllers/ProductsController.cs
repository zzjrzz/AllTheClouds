using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models.Calculators;
using AllTheClouds.Models.DTO;
using AllTheClouds.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllTheClouds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResponse>> Get_Marked_Up_Products()
        {
            var products = await _productsService.ListProductsAsync();
            var markupCalculator = new PriceCalculator(new MarkupPriceCalculator(1.2m));
            return markupCalculator.Calculate(products);
        }

        [HttpGet("{targetCurrency}")]
        public async Task<IEnumerable<ProductResponse>> Get_Products_In_Currency([FromRoute] string targetCurrency)
        {
            var markedUpProducts = await Get_Marked_Up_Products();
            if (targetCurrency == "AUD")
                return markedUpProducts;
            var foreignExchangeRates = await _productsService.ListFxRatesAsync();
            var calculator =
                new PriceCalculator(new ForeignExchangeRateCalculator(foreignExchangeRates, "AUD", targetCurrency));
            return calculator.Calculate(markedUpProducts);
        }
    }
}