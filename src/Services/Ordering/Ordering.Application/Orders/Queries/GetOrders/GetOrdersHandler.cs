using CommonOperations.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext):IQueryHandler<GetOrdersQuery,GetOrdersResult>
{

    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageNumber = query.PaginationRequest.PageNumber;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
                pageNumber,
                pageSize,
                totalCount,
                orders.ToOrderDtoList()
                )
            );
    }
}