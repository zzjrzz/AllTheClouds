using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models;
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
        public async Task<IEnumerable<ProductResponse>> GetProducts(
            [FromQuery] decimal markupMultiplier,
            [FromQuery] Currency sourceCurrency = Currency.AUD,
            [FromQuery] Currency targetCurrency = Currency.AUD)
        {
            var products = await _productsService.ListProductsAsync();
            var currencyRates = await _currencyService.ListFxRatesAsync();
            return products
                .MarkUpPrices(markupMultiplier)
                .ConvertCurrency(currencyRates, sourceCurrency, targetCurrency);
        }
    }
}