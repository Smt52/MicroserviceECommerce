

namespace ShoppingCart.API.Cart.StoreCart
{
    public record StoreCartCommand(Basket Cart) : ICommand<StoreCartResult>;

    public record StoreCartResult(string UserName);

    public class StoreCartCommandValidator : AbstractValidator<StoreCartCommand>
    {
        public StoreCartCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    public class StoreCartCommandHandler(IBasketRepository _repository) : ICommandHandler<StoreCartCommand, StoreCartResult>
    {
        public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
        {
            Basket basket = command.Cart;

            await _repository.StoreBasket(basket, cancellationToken);

            return new StoreCartResult(command.Cart.UserName);
        }
    }
}