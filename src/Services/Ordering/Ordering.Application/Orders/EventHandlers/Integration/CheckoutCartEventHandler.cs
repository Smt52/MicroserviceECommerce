using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class CheckoutCartEventHandler
    (ISender sender,ILogger<CheckoutCartEventHandler> logger)
    :IConsumer<ShoppingCartCheckoutEvent>
{
    public async Task Consume(ConsumeContext<ShoppingCartCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event Handled:{IntegrationEvent}",context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);

    }





    private static CreateOrderCommand MapToCreateOrderCommand(ShoppingCartCheckoutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine,
            message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV,(PaymentMethod)message.PaymentMethod);

        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            message.UserName,
            addressDto,
            addressDto,
            paymentDto,
            OrderStatus.Pending,
            OrderItems:
            [
                //For development only you can use message to get this items
                new OrderItemDto(orderId,new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),2,950),
                new OrderItemDto(orderId,new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),1,840)
            ]);

        return new CreateOrderCommand(orderDto);
    }
}