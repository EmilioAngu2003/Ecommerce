using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IRepository<Product> _productRepository;

    public GetProductByIdQueryHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product != null && !product.IsActive) return null;

        return product;
    }
}