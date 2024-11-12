namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

//Using FluentValidation For Validate Order Object
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    //Ctor for validation
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required for order");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer is required for creating an order");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order items must specified for an order");
    }
}