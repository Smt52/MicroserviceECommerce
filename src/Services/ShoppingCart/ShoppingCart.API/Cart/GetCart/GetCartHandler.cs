using ShoppingCart.API.Data;

namespace ShoppingCart.API.Cart.GetCart
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(Basket Cart);

    public class GetCartQueryHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetBasket(query.UserName, cancellationToken);

            return new GetBasketResult(basket);
        }
    }
}