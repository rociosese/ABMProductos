using CSRPractica.Models.DTOs.Requests;
using CSRPractica.Models.DTOs.Responses;
using CSRPractica.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using CSRPractica.Services.Interfaces;

namespace CSRPractica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

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
            if (_service.ProductNameExists(dto.Name))
            {
                return Conflict("Ya existe un producto con ese nombre.");
            }

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

        [HttpGet("search")]
        public ActionResult<List<ProductForReadDto>> SearchByName([FromQuery] string name)
        {
            List<ProductForReadDto> products = _service.SearchProductsByName(name);

            return Ok(products);
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var stats = _service.GetStats();

            return Ok(stats);
        }
    }
}