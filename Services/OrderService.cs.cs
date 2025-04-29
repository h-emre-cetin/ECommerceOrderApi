using ECommerceOrderApi.DTOs;
using ECommerceOrderApi.Models;
using ECommerceOrderApi.Repositories.Interfaces;
using ECommerceOrderApi.Services.Interface;

namespace ECommerceOrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders.Select(MapOrderToDto);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return order != null ? MapOrderToDto(order) : null;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            foreach (var item in orderDto.Items)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found");
                }

                if (product.StockQuantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {item.Quantity}");
                }
            }

            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var item in orderDto.Items)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product != null)
                {
                    var orderItem = new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price
                    };

                    order.Items.Add(orderItem);
                    totalAmount += orderItem.UnitPrice * orderItem.Quantity;

                    await _productRepository.UpdateProductStockAsync(item.ProductId, item.Quantity);
                }
            }

            order.TotalAmount = totalAmount;

            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            return MapOrderToDto(createdOrder);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }

        private static OrderDto MapOrderToDto(Order order)
        {
            return new OrderDto(
                order.Id,
                order.UserId,
                order.OrderDate,
                order.TotalAmount,
                order.Status.ToString(),
                order.Items.Select(item => new OrderItemDto(
                    item.ProductId,
                    item.Product?.Name,
                    item.Quantity,
                    item.UnitPrice
                ))
            );
        }
    }
}
