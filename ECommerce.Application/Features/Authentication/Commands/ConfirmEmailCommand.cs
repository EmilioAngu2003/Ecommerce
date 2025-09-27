using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands;

public class ConfirmEmailCommand : IRequest<bool>
{
    public string UserId { get; set; }
    public string EmailToken { get; set; }
}
