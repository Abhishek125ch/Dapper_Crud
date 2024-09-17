using DapperDockerAPI.Repositories.RepositoryAbstract;
using DapperDockerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperDockerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult>DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProductAsync(id);
            if(result>0)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]

        public async Task<IActionResult>CreateProduct(Products product)
        {
            var created_product = await _productRepository.CreateProductAsync(product);

            if (created_product > 0)
            {
                // Assuming that GetProductById is the action to retrieve the product details
                // Construct the URI of the newly created product
                var uri = Url.Action(nameof(GetProductById), new { id = created_product });

                // Return 201 Created with the Location header set to the URI of the created resource
                return Created(uri, null); // The second parameter is the response body; `null` for no body
            }
            else
            {
                return BadRequest();    
            }
        }

        [HttpPut]

        public async Task<IActionResult>UpdateProduct(Products product)
        {
    
            var new_product = await _productRepository.UpdateProductAsync(product);
            if(new_product>0)
            {
                return NoContent();
            }
            else
            {
                return NotFound();  
            }
        }
    }
}
