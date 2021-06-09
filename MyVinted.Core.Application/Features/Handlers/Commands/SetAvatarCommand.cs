using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class SetAvatarCommand : IRequestHandler<SetAvatarRequest, SetAvatarResponse>
    {
        private readonly IAccountManager accountManager;

        public SetAvatarCommand(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public async Task<SetAvatarResponse> Handle(SetAvatarRequest request, CancellationToken cancellationToken)
        {
            var avatarUrl = await accountManager.SetAvatar(request.Avatar);

            return avatarUrl != null ? new SetAvatarResponse { AvatarUrl = avatarUrl }
                : throw new UploadFileException("Error occurred during uploading avatar");
        }
    }
}