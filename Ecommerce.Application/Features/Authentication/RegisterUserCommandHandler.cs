using ECommerce.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Authentication;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;

    public RegisterUserCommandHandler(UserManager<IdentityUser> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new IdentityUser
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = $"{request.BaseUrl}/api/auth/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";
            var emailBody = $"Por favor, confirma tu correo electrónico haciendo clic en el siguiente enlace: {confirmationLink}";
            await _emailService.SendEmailAsync(user.Email, "Confirmación de Correo Electrónico", emailBody);
        }

        return result;
    }
}