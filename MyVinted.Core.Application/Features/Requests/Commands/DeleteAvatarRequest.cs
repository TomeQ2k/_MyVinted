using MediatR;
using MyVinted.Core.Application.Features.Responses.Commands;

namespace MyVinted.Core.Application.Features.Requests.Commands
{
    public record DeleteAvatarRequest : IRequest<DeleteAvatarResponse>
    { }
}