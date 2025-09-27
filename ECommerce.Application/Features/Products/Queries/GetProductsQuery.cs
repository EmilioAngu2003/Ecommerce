using ECommerce.Core.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries;

public class GetProductsQuery : IRequest<IReadOnlyList<Product>>
{
}