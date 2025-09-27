using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<Unit>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public IEnumerable<string> Roles { get; set; }
}
