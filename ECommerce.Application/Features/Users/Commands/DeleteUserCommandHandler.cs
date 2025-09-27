using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ECommerce.Application.Features.Users.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ClaimsPrincipal _claimsPrincipal;

    public DeleteUserCommandHandler(UserManager<IdentityUser> userManager, ClaimsPrincipal claimsPrincipal)
    {
        _userManager = userManager;
        _claimsPrincipal = claimsPrincipal;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null) return Unit.Value;
        if (_claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) == user.Id) return Unit.Value;

        await _userManager.DeleteAsync(user);

        return Unit.Value;
    }
}
