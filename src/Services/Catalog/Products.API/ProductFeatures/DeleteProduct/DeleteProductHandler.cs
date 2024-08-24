namespace Products.API.ProductFeatures.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResponse>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Could not fetch Id");
        }

    }

    public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResponse>
    {
        public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Product with Id:{@Product} has been deleted.", command);

            await session.BeginTransactionAsync(cancellationToken);
            session.Delete<Product>(command.Id);

            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResponse(true);
        }
    }
}