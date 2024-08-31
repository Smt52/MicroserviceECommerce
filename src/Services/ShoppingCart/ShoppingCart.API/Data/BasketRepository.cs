namespace ShoppingCart.API.Data
{
    public class BasketRepository(IDocumentSession session)
        : IBasketRepository
    {
        public async Task<Basket> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<Basket>(userName, cancellationToken);
            
            return basket is null ? throw new BasketNotFoundException(userName):basket;
        }

        public async Task<Basket> StoreBasket(Basket basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<Basket>(userName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}