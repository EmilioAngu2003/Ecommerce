using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Features.Authentication.Commands;

public class SendPasswordResetLinkCommand : IRequest<Unit>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
