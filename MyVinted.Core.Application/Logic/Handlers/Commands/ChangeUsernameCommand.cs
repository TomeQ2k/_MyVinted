using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class ChangeUsernameCommand : IRequestHandler<ChangeUsernameRequest, ChangeUsernameResponse>
    {
        private readonly IAccountManager accountManager;
        private readonly IAuthValidationService authValidationService;

        public ChangeUsernameCommand(IAccountManager accountManager, IAuthValidationService authValidationService)
        {
            this.accountManager = accountManager;
            this.authValidationService = authValidationService;
        }

        public async Task<ChangeUsernameResponse> Handle(ChangeUsernameRequest request, CancellationToken cancellationToken)
        {
            if (await authValidationService.UsernameExists(request.NewUsername))
                throw new DuplicateException("Username already exists", ErrorCodes.UsernameExists);

            var usernameChanged = await accountManager.ChangeUsername(request.NewUsername);

            return usernameChanged ? new ChangeUsernameResponse()
                : throw new ProfileUpdateException("Changing username failed");
        }
    }
}