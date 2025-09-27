using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IRepository<Product> _productRepository;

    public CreateProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            IsActive = request.IsActive
        };

        await _productRepository.AddAsync(newProduct);

        return newProduct;
    }
}