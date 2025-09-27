using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Features.Roles.Commands;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Unit>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (!await _roleManager.RoleExistsAsync(request.RoleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(request.RoleName));
        }
        return Unit.Value;
    }
}
