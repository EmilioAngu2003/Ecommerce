using Ecommerce.Core.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Products;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }
}