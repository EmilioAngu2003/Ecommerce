using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Features.Authentication.Commands;

public class ActivateAccountCommand : IRequest<IdentityResult>
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string EmailToken { get; set; }
    [Required]
    public string PasswordToken { get; set; }
    [Required]
    public string Password { get; set; }
}
