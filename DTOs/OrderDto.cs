namespace ECommerceOrderApi.DTOs
{
    public record OrderDto(
    int Id,
    string UserId,
    DateTime OrderDate,
    decimal TotalAmount,
    string Status,
    IEnumerable<OrderItemDto> Items
);
}
