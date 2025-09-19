using Ecommerce.Application.Features.Products;
using Ecommerce.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id}", Name = "GetProductByIdRoute")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery { Id = id });

        if (product == null) return NotFound();

        return Ok(product);
    }
}
