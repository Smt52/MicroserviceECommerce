using Mapster;

namespace ShoppingCart.API.Cart.GetCart
{
    public record GetBasketResponse(Basket Cart);

    public class GetCartEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            }).WithName("GetShoppingCartForUSer")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Cart User")
            .WithDescription("Just for fetch a shopping cart"); ;
        }
    }
}