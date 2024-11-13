namespace ShoppingCart.API.Cart.CheckoutCart;

public record CheckoutCartRequest(CheckoutCartDto BasketCheckoutDto);

public record CheckoutCartResponse(bool IsSuccess);

public class CheckoutCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutCartRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutCartCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckoutCartResponse>();

                return Results.Ok(response);
            })
            .WithName("CheckoutCart")
            .Produces<CheckoutCartResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Checks out current cart");
    }
}