namespace ShoppingCart.API.Cart.DeleteCart
{
    public record DeleteCartCommand(string UserName) : ICommand<DeleteCartResult>;

    public record DeleteCartResult(bool IsSuccess);

    public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
    {
        public DeleteCartCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserNAme cannot be null");
        }
    }

    public class DeleteCartCommandHandler(IBasketRepository _repository) : ICommandHandler<DeleteCartCommand, DeleteCartResult>
    {
        public async Task<DeleteCartResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteBasket(request.UserName, cancellationToken);
            
            return new DeleteCartResult(true);
        }
    }
}