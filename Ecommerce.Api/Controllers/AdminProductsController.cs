using Ecommerce.Application.Features.Products;
using Ecommerce.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/admin/products")]
public class AdminProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(CreateProductCommand command)
    {
        var newProduct = await _mediator.Send(command);

        return CreatedAtRoute("GetProductByIdRoute", new { id = newProduct.Id }, newProduct);
    }
}