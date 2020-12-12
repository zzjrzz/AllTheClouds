using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllTheClouds.Models;
using AllTheClouds.Models.Calculators;
using AllTheClouds.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllTheClouds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductsResponse>> Get_Products()
        {
            var products = await _productsService.ListProductsAsync();
            var calculator = new PriceCalculator(new MarkupPriceCalculator(1.2m));
            return calculator.Calculate(products);
        }
    }
}