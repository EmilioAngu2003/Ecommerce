using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IReadOnlyList<Product>>
{
    private readonly IRepository<Product> _productRepository;

    public GetProductsQueryHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var activeProducts = await _productRepository.GetAsync(p => p.IsActive);

        return activeProducts;
    }
}