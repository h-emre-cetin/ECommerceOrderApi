using ECommerceOrderApi.Models;

namespace ECommerceOrderApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(int productId);
        
        Task UpdateProductStockAsync(int productId, int quantityChange);
    }
}
