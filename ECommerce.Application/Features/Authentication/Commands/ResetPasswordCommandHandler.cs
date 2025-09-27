using ECommerce.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Features.Authentication.Commands;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IdentityResult>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUrlBuilderService _urlBuilderService;

    public ResetPasswordCommandHandler(
        UserManager<IdentityUser> userManager,
        IUrlBuilderService urlBuilderService)
    {
        _userManager = userManager;
        _urlBuilderService = urlBuilderService;
    }

    public async Task<IdentityResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "Restablecimiento fallido. El enlace es inválido." });

        string originalPasswordToken = _urlBuilderService.DecodeToken(request.PasswordToken);

        var result = await _userManager.ResetPasswordAsync(user, originalPasswordToken, request.NewPassword);

        if (result.Succeeded)
            return IdentityResult.Success;

        return result;
    }
}
