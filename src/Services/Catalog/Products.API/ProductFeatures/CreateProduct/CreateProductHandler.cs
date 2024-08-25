namespace Products.API.ProductFeatures.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImagePath,
    decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommonValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommonValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImagePath).NotEmpty().WithMessage("ImagePath is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImagePath = command.ImagePath,
            Price = command.Price,
        };
        
        await session.BeginTransactionAsync(cancellationToken);
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //Return Result
        return new CreateProductResult(product.Id);
    }
}