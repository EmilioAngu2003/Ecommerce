using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Features.Authentication.Commands;

public class ResetPasswordCommand : IRequest<IdentityResult>
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string PasswordToken { get; set; }

    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; }
}
