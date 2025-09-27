using ECommerce.Core.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }
}