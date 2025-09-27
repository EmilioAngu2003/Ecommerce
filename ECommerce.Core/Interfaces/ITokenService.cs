using ECommerce.Core.Models;

namespace ECommerce.Core.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(User user);
}
