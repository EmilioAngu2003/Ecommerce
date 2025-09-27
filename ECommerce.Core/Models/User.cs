namespace ECommerce.Core.Models;

public record User(string Id, string UserName, string Email, IEnumerable<string> Roles);
