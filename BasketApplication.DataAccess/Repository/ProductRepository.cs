using BasketApplication.Entities.Abstract;
using BasketApplication.Entities.Dtos.ProductDto;
using BasketApplication.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketApplication.DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product productModel, int quantity)
        {
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();
            await _context.ProductStocks.AddAsync(new ProductStock
            {
                ProductId = productModel.Id,
                Quantity = quantity
            });
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return null;
            }
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public Task<List<Product>> GetAllStocksAsync()
        {
            return _context.Products.Include(x => x.Stock).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto)
        {
            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return null;
            }

            productModel.Name = productDto.Name;
            productModel.Price = productDto.Price;

            await _context.SaveChangesAsync();

            return productModel;
        }
    }
}
