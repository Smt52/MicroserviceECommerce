﻿namespace Products.API.ProductFeatures.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            
            await session.BeginTransactionAsync(cancellationToken);
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(query.Id);
            }
            var result = new GetProductByIdResult(product);
            return result;
        }
    }
}