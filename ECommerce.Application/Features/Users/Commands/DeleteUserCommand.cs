using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Features.Users.Commands;

public class DeleteUserCommand : IRequest<Unit>
{
    [Required]
    public string UserId { get; set; }
}
