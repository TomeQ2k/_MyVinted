using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class ChangeEmailCommand : IRequestHandler<ChangeEmailRequest, ChangeEmailResponse>
    {
        private readonly IAccountManager accountManager;
        private readonly IAuthValidationService authValidationService;

        public ChangeEmailCommand(IAccountManager accountManager, IAuthValidationService authValidationService)
        {
            this.accountManager = accountManager;
            this.authValidationService = authValidationService;
        }

        public async Task<ChangeEmailResponse> Handle(ChangeEmailRequest request, CancellationToken cancellationToken)
        {
            if (await authValidationService.EmailExists(request.NewEmail))
                throw new DuplicateException("Email address already exists", ErrorCodes.EmailExists);

            var emailChanged = await accountManager.ChangeEmail(request.NewEmail, request.Token);

            return emailChanged ? new ChangeEmailResponse()
                : throw new ProfileUpdateException("Error occurred during changing email address");
        }
    }
}