using CoreServices.DTO;
using CoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ILogger _logger;
        public ProductController(ProductService productService, ILogger logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ProductDTO product)
        {
              try
              {
                var result = await _productService.AddAsync(product);
                if (result != null)
                {
                    return CreatedAtAction("Get", new { id = result.Id }, result);
                }
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
              }
              catch (Exception ex)
              {
                    _logger.LogError(ex, ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
              }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                if (products != null)
                {
                    return Ok(products);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
