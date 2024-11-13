
namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext)
:ICommandHandler<UpdateOrderCommand,UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        //Update existing order
        var orderId = OrderId.Of(command.Order.Id);
        
        var order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken);

        if (order == null) throw new OrderNotFoundException(command.Order.Id);

        UpdateOrderWithNewOrderValues(order, command.Order);
        
        //Save changes
        dbContext.Orders.Update(order);
        var successfulCount = await dbContext.SaveChangesAsync(cancellationToken);
        
        //return result
        if (successfulCount>0)
        {
            return new UpdateOrderResult(true);
        }

        return new UpdateOrderResult(false);
    }

    private static void UpdateOrderWithNewOrderValues(Order order, OrderDto orderDto)
    {
        var newShippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName, 
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country, 
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode);
        var newBillingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode
            );
        var newPaymentInfo = Payment.Of(
            orderDto.Payment.CardName,
            orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration,
            orderDto.Payment.Cvv,
            orderDto.Payment.PaymentMethod
            );
        
        order.Update(OrderName.Of(orderDto.OrderName),newShippingAddress,newBillingAddress,newPaymentInfo,orderDto.Status);
    }
}