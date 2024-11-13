using MassTransit;
using Messaging.Events;

namespace ShoppingCart.API.Cart.CheckoutCart;

public record CheckoutCartCommand(CheckoutCartDto CheckoutCartDto) 
    : ICommand<CheckoutCartResult>;

public record CheckoutCartResult(bool IsSuccess);

public class CheckoutCartValidator
    : AbstractValidator<CheckoutCartCommand>
{
    public CheckoutCartValidator()
    {
        RuleFor(x => x.CheckoutCartDto).NotNull().WithMessage("Cart cannot be empty!");
        RuleFor(x => x.CheckoutCartDto.UserName).NotEmpty().WithMessage("User must be selected!");
    }
}


public class CheckoutCartHandler (IBasketRepository repository,IPublishEndpoint publishEndpoint)
    :ICommandHandler<CheckoutCartCommand,CheckoutCartResult>
{
    public async Task<CheckoutCartResult> Handle(CheckoutCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasket(command.CheckoutCartDto.UserName, cancellationToken);
        if (cart==null)
        {
            return new CheckoutCartResult(false);
        }

        var eventMessage = command.CheckoutCartDto.Adapt<ShoppingCartCheckoutEvent>();
        eventMessage.TotalPrice = cart.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(command.CheckoutCartDto.UserName, cancellationToken);

        return new CheckoutCartResult(true);
    }
}