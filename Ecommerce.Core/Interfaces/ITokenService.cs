using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(User user);
}
