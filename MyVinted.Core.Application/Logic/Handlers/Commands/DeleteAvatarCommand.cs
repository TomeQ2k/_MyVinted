using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class DeleteAvatarCommand : IRequestHandler<DeleteAvatarRequest, DeleteAvatarResponse>
    {
        private readonly IAccountManager accountManager;

        public DeleteAvatarCommand(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public async Task<DeleteAvatarResponse> Handle(DeleteAvatarRequest request, CancellationToken cancellationToken)
        => await accountManager.DeleteAvatar()
            ? new DeleteAvatarResponse()
            : throw new DeleteFileException("Error occurred during deleting avatar");
    }
}