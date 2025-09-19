using Ecommerce.Core.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Products;

public class GetProductsQuery : IRequest<IReadOnlyList<Product>>
{
}