using Products.API.ProductFeatures.GetProductById;

namespace Products.API.ProductFeatures.GetProductByCategoryName
{
    public record GetProductByCategoryNameQuery(string categoryName) : IQuery<GetProductByCategoryNameResult>;

    public record GetProductByCategoryNameResult(IEnumerable<Product> Products);

    public class GetProductByCategoryNamQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryNameQuery, GetProductByCategoryNameResult>
    {
        public async Task<GetProductByCategoryNameResult> Handle(GetProductByCategoryNameQuery query, CancellationToken cancellationToken)
        {
            
            await session.BeginTransactionAsync(cancellationToken);

            var product = await session.Query<Product>().Where(p => p.Category.Contains(query.categoryName)).ToListAsync(token: cancellationToken);
            
            var result = new GetProductByCategoryNameResult(product);

            return result;
        }
    }
}