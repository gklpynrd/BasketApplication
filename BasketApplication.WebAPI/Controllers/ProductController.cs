using BasketApplication.Entities.Abstract;
using BasketApplication.Entities.Dtos.Product;
using BasketApplication.Helpers.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BasketApplication.WebAPI.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            var productDto = products.Select(s => s.ToProductDto());

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.ToProductDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestDto productDto)
        {
            var productModel = productDto.ToProductFromCreateDto();

            await _productRepository.CreateAsync(productModel);

            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequestDto productDto)
        {
            var productModel = await _productRepository.UpdateAsync(id, productDto);

            if (productModel == null)
                return NotFound();

            return Ok(productModel.ToProductDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var productModel = await _productRepository.DeleteAsync(id);
            if (productModel == null)
                return NotFound();
            return NoContent();
        }
    }
}
