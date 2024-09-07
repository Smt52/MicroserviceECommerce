namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value)
    {
        Value = value;
    }

    //Factory method for OrderItemId
    public static OrderItemId Of(Guid value)
    {
        return new OrderItemId(value);
    }
}