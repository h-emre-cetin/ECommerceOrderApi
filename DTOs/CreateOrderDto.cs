namespace ECommerceOrderApi.DTOs
{
    public record CreateOrderDto(
    string UserId,
    IEnumerable<CreateOrderItemDto> Items
    );

    public record CreateOrderItemDto(
        int ProductId,
        int Quantity
    );
}
