using CSRPractica.Models.DTOs.Requests;
using CSRPractica.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace CSRPractica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private ProductService _service = new ProductService();

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _service.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(ProductForCreateDto dto)
        {
            var product = _service.CreateProduct(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductForUpdateDto dto)
        {
            var product = _service.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            _service.UpdateProduct(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _service.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            _service.DeleteProduct(id);

            return NoContent();
        }
    }
}