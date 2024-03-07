using BasketApplication.Entities.Dtos.Product;
using BasketApplication.Entities.Models;

namespace BasketApplication.Helpers.Mappers
{
    public static class ProductMappers
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Price = productModel.Price
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductRequestDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            };
        }
    }
}
