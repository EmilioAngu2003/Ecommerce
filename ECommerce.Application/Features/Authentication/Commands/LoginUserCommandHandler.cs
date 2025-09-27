using ECommerce.Core.Interfaces;
using ECommerce.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Features.Authentication.Commands;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var identityUser = await _userManager.FindByNameAsync(request.UserName);

        if (identityUser == null || !await _userManager.IsEmailConfirmedAsync(identityUser))
            return null;

        var result = await _signInManager.CheckPasswordSignInAsync(identityUser, request.Password, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(identityUser);
            var user = new User(identityUser.Id, identityUser.UserName, identityUser.Email, roles);
            return await _tokenService.CreateTokenAsync(user);
        }

        return null;
    }
}
