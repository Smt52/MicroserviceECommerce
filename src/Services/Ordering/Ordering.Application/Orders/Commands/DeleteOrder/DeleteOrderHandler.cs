namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand,DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        
        //Delete Order 
        //Don't need to include entities and order has a pk so more performant query will be FindAsync
        var order = await dbContext.Orders.FindAsync([OrderId.Of(command.OrderId)], cancellationToken);
        if (order==null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }
        dbContext.Orders.Remove(order);
        //Save Changes
       var successfulCount =  await dbContext.SaveChangesAsync(cancellationToken);
        //Return Result
        if (successfulCount>0)
        {
            return new DeleteOrderResult(true);
        }

        return new DeleteOrderResult(false);
    }
}