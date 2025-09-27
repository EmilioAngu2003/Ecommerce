using ECommerce.Application.Interfaces;
using ECommerce.Core.Configuration;
using ECommerce.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ECommerce.Application.Features.Authentication.Commands;

public class SendPasswordResetLinkCommandHandler : IRequestHandler<SendPasswordResetLinkCommand, Unit>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUrlBuilderService _urlBuilderService;
    private readonly FrontEndRoutes _frontEndRoutes;

    public SendPasswordResetLinkCommandHandler(
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

    public async Task<Unit> Handle(SendPasswordResetLinkCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null) return Unit.Value;

        var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var resetLink = _urlBuilderService.CreateLink(_frontEndRoutes.ResetPasswordUrl)
                .AddParameter("userId", user.Id)
                .AddEncodedToken("passwordToken", passwordToken)
                .Build();

        await _emailService.SendEmailAsync(request.Email, "Restablecer Contraseña", resetLink);

        return Unit.Value;
    }
}
