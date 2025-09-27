using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Features.Roles.Commands;

public class CreateRoleCommand : IRequest<Unit>
{
    [Required]
    [StringLength(50)]
    public string RoleName { get; set; }
}
