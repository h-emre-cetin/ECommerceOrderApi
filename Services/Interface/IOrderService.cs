using ECommerceOrderApi.DTOs;

namespace ECommerceOrderApi.Services.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId);
        
        Task<OrderDto?> GetOrderByIdAsync(int orderId);
        
        Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto);
        
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
