using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Features.Authentication;

public class LoginUserCommand : IRequest<string>
{
    [Required]
    public string UserName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
