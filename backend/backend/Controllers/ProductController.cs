using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get() => await _productService.GetAllProducts();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            // If no Id is provided, MongoDB will generate one automatically
            if (product.Id == ObjectId.Empty)
            {
                product.Id = ObjectId.GenerateNewId();  // Generate new ObjectId
            }

            await _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id.ToString() }, product);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();

            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
