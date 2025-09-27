using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Features.Authentication.Commands;

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
