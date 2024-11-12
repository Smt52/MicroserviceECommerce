﻿namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    //Handle method will be triggered from Mediator
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        //create order entity from command obj

        var order = CreateNewOrder(command.Order);

        //save order to db

        await dbContext.Orders.AddAsync(order, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        //return result
        return new CreateOrderResult(order.Id.Value);
    }

    private static Order CreateNewOrder(OrderDto order)
    {
        var shippingAddress = Address.Of(
            order.ShippingAddress.FirstName,
            order.ShippingAddress.LastName,
            order.ShippingAddress.EmailAddress,
            order.ShippingAddress.AddressLine,
            order.ShippingAddress.Country,
            order.ShippingAddress.State,
            order.ShippingAddress.ZipCode
        );

        var billingAddress = Address.Of(
            order.BillingAddress.FirstName,
            order.BillingAddress.LastName,
            order.BillingAddress.EmailAddress,
            order.BillingAddress.AddressLine,
            order.BillingAddress.Country,
            order.BillingAddress.State,
            order.BillingAddress.ZipCode
        );

        var newOrder = Order.Create(
            id:OrderId.Of(Guid.NewGuid()),
            customerId:CustomerId.Of(order.CustomerId),
            orderName:OrderName.Of(order.OrderName),
            shippingAddress:shippingAddress,
            billingAddress:billingAddress,
            payment:Payment.Of(
                order.Payment.CardName,
                order.Payment.CardNumber,
                order.Payment.Expiration,
                order.Payment.Cvv,
                order.Payment.PaymentMethod)
            );
        foreach (var orderItem in order.OrderItems)
        {
            newOrder.Add(ProductId.Of(
                orderItem.ProductId),
                orderItem.Quantity,
                orderItem.Price 
                );
        }

        return newOrder;
    }
}