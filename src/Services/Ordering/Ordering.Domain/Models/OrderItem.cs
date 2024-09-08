namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        internal OrderItem(
            OrderId orderId,
            ProductId productId,
            decimal price,
            int quantity)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            ProductId = productId;
            OrderId = orderId;
            Price = price;
            Quantity = quantity;
        }

        public ProductId ProductId { get; private set; } = default!;

        public OrderId OrderId { get; private set; } = default!;
        public Order? Order { get; private set; }
        public decimal Price { get; private set; }

        public int Quantity { get; private set; }
    }
}