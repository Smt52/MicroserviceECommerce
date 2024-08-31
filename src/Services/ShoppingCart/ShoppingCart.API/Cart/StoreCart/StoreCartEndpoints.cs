using Mapster;

namespace ShoppingCart.API.Cart.StoreCart
{
    public record StoreCartRequest(Basket Cart);

    public record StoreCartResponse(string UserName);

    public class StoreCartEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreCartRequest req, ISender sender) =>
            {
                var command = req.Adapt<StoreCartCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<StoreCartResponse>();

                return Results.Created($"/basket/{response.UserName}", response);
            }).WithName("CreateShoppingCart")
            .Produces<StoreCartResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}