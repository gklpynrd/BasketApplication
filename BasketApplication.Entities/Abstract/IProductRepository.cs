using BasketApplication.Entities.Dtos.ProductDto;
using BasketApplication.Entities.Models;

namespace BasketApplication.Entities.Abstract
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product productModel, int quantity);
        Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto);
        Task<Product?> DeleteAsync(int id);
        Task<List<Product>> GetAllStocksAsync();
    }
}
