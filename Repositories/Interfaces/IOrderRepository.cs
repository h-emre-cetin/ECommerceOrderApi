using ECommerceOrderApi.Models;

namespace ECommerceOrderApi.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        
        Task<Order?> GetOrderByIdAsync(int orderId, bool includeItems = true);
        
        Task<Order> CreateOrderAsync(Order order);
        
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
