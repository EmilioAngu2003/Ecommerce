using ECommerce.Application.Interfaces;
using ECommerce.Core.Configuration;
using ECommerce.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ECommerce.Application.Features.Authentication.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUrlBuilderService _urlBuilderService;
    private readonly FrontEndRoutes _frontEndRoutes;

    public RegisterUserCommandHandler(
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

            var confirmationLink = _urlBuilderService.CreateLink(_frontEndRoutes.ConfirmEmailUrl)
                .AddParameter("userId", user.Id)
                .AddEncodedToken("emailToken", token)
                .Build();

            var emailBody = $"Por favor, confirma tu correo electrónico haciendo clic en el siguiente enlace: {confirmationLink}";
            await _emailService.SendEmailAsync(user.Email, "Confirmación de Correo Electrónico", emailBody);
        }

        return result;
    }
}