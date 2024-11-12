namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public record GetOrdersByName(string Name)
    : IQuery<GetOrdersByNameResult>;

public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);