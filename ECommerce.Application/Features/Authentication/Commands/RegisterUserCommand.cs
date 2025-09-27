using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Features.Authentication.Commands;

public class RegisterUserCommand : IRequest<IdentityResult>
{
    [Required]
    public string UserName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string? BaseUrl { get; set; }
}
