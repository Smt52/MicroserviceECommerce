namespace Products.API.ProductFeatures.CreateProduct;

public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImagePath,
    decimal Price
);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
                {
                    //Request is mapped through mapster to command
                    var command = request.Adapt<CreateProductCommand>();
                    
                    //Send command object and trigger the handler
                    var result = await sender.Send(command);
                    
                    //Get response and map it to productresponse
                    var response = result.Adapt<CreateProductResponse>();

                    //When Create Complete then it will redirect to prdocut page with id
                    return Results.Created($"/products/{response.Id}", response);
                })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Just for create a product");

    }
}