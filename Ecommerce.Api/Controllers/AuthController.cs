using Ecommerce.Application.Features.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterUserCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        command.BaseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok("User registered successfully");
        }
        else
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(errors);
        }
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            return BadRequest("Invalid confirmation link.");

        var command = new ConfirmEmailCommand
        {
            UserId = userId,
            Token = token
        };

        var result = await _mediator.Send(command);

        if (result)
        {
            return Ok("Email confirmed successfully. You can now log in.");
        }
        else
        {
            return BadRequest("Error confirming your email. The link may be invalid or expired.");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(LoginUserCommand command)
    {
        var token = await _mediator.Send(command);

        if (token != null)
        {
            return Ok(new { Token = token });
        }
        else
        {
            return Unauthorized("Invalid credentials or unconfirmed email.");
        }
    }
}
