using System.Data;
using Products.API.ProductFeatures.CreateProduct;
using System.Reflection;

namespace Products.API.ProductFeatures.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImagePath,
    decimal Price
) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {

            #region With Reflection But Something is wrong and i could not fogured it out.
            
            // var properties = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //
            // foreach (var property in properties)
            // {
            //     
            //     if (property.PropertyType == typeof(string))
            //     {
            //         RuleFor(x => property.GetValue(x,null) as string)
            //             .NotEmpty()
            //             .WithMessage($"{property.Name} is required");
            //     }
            //     if (property.PropertyType == typeof(Guid))
            //     {
            //         RuleFor(x => property.GetValue(x,null) as Guid?)
            //             .NotEmpty()
            //             .WithMessage($"{property.Name} could not fetched");
            //     }
            //     if (property.PropertyType == typeof(List<string>))
            //     {
            //         RuleFor(x => property.GetValue(x,null)! as List<string>)
            //             .NotEmpty()
            //             .WithMessage($"{property.Name} cannot be empty")
            //             .Must(list => list.TrueForAll(item => !string.IsNullOrEmpty(item)))
            //             .WithMessage($"{property.Name} contains invalid items")
            //             .When(x => property.GetValue(x, null) != null);
            //     }
            //
            //     if (property.PropertyType == typeof(decimal))
            //     {
            //         RuleFor(x => property.GetValue(x,null) as decimal?).GreaterThan(0).WithMessage("Price must be greater than zero");
            //     }
            //     
            // }

            #endregion


            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be null");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be null");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Categories cannot be null");
            


        }
        
        
    }

    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handel called {@Command}", command);

            await session.BeginTransactionAsync(cancellationToken);
            logger.LogInformation("UpdateProductHandler transaction started...");
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null) throw new ProductNotFoundException(command.Id);

            product.Name = command.Name;
            product.Description = command.Description;
            product.Category = command.Category;
            product.ImagePath = command.ImagePath;
            product.Price = command.Price;

            session.Store<Product>(product);

            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}