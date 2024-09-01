using Discount.gRPC;

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

    public class StoreCartCommandHandler(IBasketRepository _repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreCartCommand, StoreCartResult>
    {
        public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
        {
            await ApplyDiscount(command.Cart, cancellationToken);
            await _repository.StoreBasket(command.Cart, cancellationToken);

            return new StoreCartResult(command.Cart.UserName);
        }

        private async Task ApplyDiscount(Basket basket, CancellationToken cancellationToken)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}