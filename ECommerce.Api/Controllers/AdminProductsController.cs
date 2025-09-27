using ECommerce.Application.Features.Products.Commands;
using ECommerce.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

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