using Products.API.ProductFeatures.GetProductById;

namespace Products.API.ProductFeatures.GetProductByCategoryName
{
    public record GetProductByCategoryNameResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryNameEndpoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{categoryname}", async (string categoryname, ISender sender) =>
            {
                var command = new GetProductByCategoryNameQuery(categoryname);
                var result = await sender.Send(command);
                var response = result.Adapt<GetProductByCategoryNameResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategoryName")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category Name")
            .WithDescription("Just for fetch a product with category name filter");
        }
    }
}