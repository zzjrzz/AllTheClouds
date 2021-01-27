using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models.DTO;
using AllTheClouds.Services;
using AllTheClouds.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AllTheClouds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ICurrencyService _currencyService;

        public ProductsController(IProductsService productsService, ICurrencyService currencyService)
        {
            _productsService = productsService;
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResponse>> GetMarkedUpProducts()
        {
            var products = await _productsService.ListProductsAsync();
            return products.MarkUpPrices(1.2m);
        }

        [HttpGet("{sourceCurrency}/{targetCurrency}")]
        public async Task<IEnumerable<ProductResponse>> GetProductsInCurrency([FromRoute] string sourceCurrency,
            string targetCurrency)
        {
            var markedUpProducts = await GetMarkedUpProducts();
            return markedUpProducts.ConvertCurrency(await _currencyService.ListFxRatesAsync(),
                sourceCurrency,
                targetCurrency);
        }
    }
}