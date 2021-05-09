using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class ChangePasswordCommand : IRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
    {
        private readonly IAccountManager accountManager;

        public ChangePasswordCommand(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
            => await accountManager.ChangePassword(request.OldPassword, request.NewPassword)
                ? new ChangePasswordResponse()
                : throw new ChangePasswordException("Changing password failed");
    }
}