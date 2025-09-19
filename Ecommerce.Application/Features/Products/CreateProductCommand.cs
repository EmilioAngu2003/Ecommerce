using Ecommerce.Core.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Products;

public class CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; } = true;
}