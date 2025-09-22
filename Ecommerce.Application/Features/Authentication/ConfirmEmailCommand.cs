using MediatR;

namespace Ecommerce.Application.Features.Authentication;

public class ConfirmEmailCommand : IRequest<bool>
{
    public string UserId { get; set; }
    public string Token { get; set; }
}
