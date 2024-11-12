namespace Ordering.API.Endpoints;

public record GetOrdersResponse(PaginatedResult<IEnumerable<OrderDto>> Orders);

public class GetOrders :ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest req, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(req));

            var response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);
        }).WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Get All Orders");
    }
}