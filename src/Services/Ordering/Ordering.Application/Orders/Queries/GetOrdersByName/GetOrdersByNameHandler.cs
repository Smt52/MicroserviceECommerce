
namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetOrdersByName,GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByName query, CancellationToken cancellationToken)
    {
        var orders =await dbContext.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .Where(x => x.OrderName.Value.Contains(query.Name))
            .OrderBy(x=>x.OrderName)
            .ToListAsync(cancellationToken);
        
        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }

    
}