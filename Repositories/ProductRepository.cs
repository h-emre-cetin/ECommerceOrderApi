using ECommerceOrderApi.Data;
using ECommerceOrderApi.Models;
using ECommerceOrderApi.Repositories.Interfaces;

namespace ECommerceOrderApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task UpdateProductStockAsync(int productId, int quantityChange)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.StockQuantity -= quantityChange;
                await _context.SaveChangesAsync();
            }
        }
    }
}
