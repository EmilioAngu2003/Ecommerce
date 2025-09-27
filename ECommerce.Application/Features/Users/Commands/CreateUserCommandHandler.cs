using ECommerce.Application.Interfaces;
using ECommerce.Core.Configuration;
using ECommerce.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ECommerce.Application.Features.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUrlBuilderService _urlBuilderService;
    private readonly FrontEndRoutes _frontEndRoutes;

    public CreateUserCommandHandler(
        UserManager<IdentityUser> userManager,
        IEmailService emailService,
        IUrlBuilderService urlBuilderService,
        IOptions<FrontEndRoutes> frontEndRoutes)
    {
        _userManager = userManager;
        _emailService = emailService;
        _urlBuilderService = urlBuilderService;
        _frontEndRoutes = frontEndRoutes.Value;
    }


    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new IdentityUser
        {
            UserName = request.Username,
            Email = request.Email,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(user, request.Roles);

            var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var invitationLink = _urlBuilderService.CreateLink(_frontEndRoutes.AccountActivationUrl)
                    .AddParameter("userId", user.Id)
                    .AddEncodedToken("emailToken", emailConfirmToken)
                    .AddEncodedToken("passwordToken", passwordResetToken)
                    .Build();

            var emailBody = $"Por favor, activa tu cuenta y establece tu contraseña haciendo clic en el siguiente enlace: <a href='{invitationLink}'>Activar Cuenta</a>";
            await _emailService.SendEmailAsync(user.Email, "Invitación a mi E-Commerce", emailBody);
        }

        return Unit.Value;
    }
}
