using BasketApplication.Entities.Dtos.Product;
using BasketApplication.Entities.Models;

namespace BasketApplication.Entities.Abstract
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product productModel);
        Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto);
        Task<Product?> DeleteAsync(int id);


    }
}
