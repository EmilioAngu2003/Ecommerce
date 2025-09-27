using ECommerce.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Features.Authentication.Commands;

public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, IdentityResult>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUrlBuilderService _urlBuilderService;

    public ActivateAccountCommandHandler(
        UserManager<IdentityUser> userManager,
        IUrlBuilderService urlBuilderService)
    {
        _userManager = userManager;
        _urlBuilderService = urlBuilderService;
    }

    public async Task<IdentityResult> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Activación fallida. Por favor, intente de nuevo." });

        string originalEmailToken = _urlBuilderService.DecodeToken(request.EmailToken);

        var confirmResult = await _userManager.ConfirmEmailAsync(user, originalEmailToken);
        if (!confirmResult.Succeeded) return confirmResult;

        string originalPasswordToken = _urlBuilderService.DecodeToken(request.PasswordToken);

        var resetResult = await _userManager.ResetPasswordAsync(user, originalPasswordToken, request.Password);
        if (!resetResult.Succeeded) return resetResult;

        return IdentityResult.Success;
    }
}
