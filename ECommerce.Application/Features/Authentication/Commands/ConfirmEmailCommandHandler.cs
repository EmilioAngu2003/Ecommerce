using ECommerce.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Features.Authentication.Commands;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUrlBuilderService _urlBuilderService;

    public ConfirmEmailCommandHandler(
        UserManager<IdentityUser> userManager,
        IUrlBuilderService urlBuilderService)
    {
        _userManager = userManager;
        _urlBuilderService = urlBuilderService;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null) return false;

        if (user.EmailConfirmed) return false;

        string originalToken = _urlBuilderService.DecodeToken(request.EmailToken);

        var result = await _userManager.ConfirmEmailAsync(user, originalToken);

        return result.Succeeded;
    }
}