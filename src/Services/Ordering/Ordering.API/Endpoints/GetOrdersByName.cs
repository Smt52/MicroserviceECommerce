namespace Ordering.API.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName :ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            // Note: This name is already used elsewhere in the code by mistake, which can lead to ambiguity. 
            // Rename to ensure unique and descriptive naming.
            var result =await sender.Send(new Application.Orders.Queries.GetOrdersByName.GetOrdersByName(orderName));

            var response = result.Adapt<GetOrdersByNameResponse>();

            return Results.Ok(response);
            
        }).WithName("GetOrdersByName")
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Get Orders With Specified Name");
    }
}