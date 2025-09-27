using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Features.Authentication.Commands;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly UserManager<IdentityUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null) return false;

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        return result.Succeeded;
    }
}