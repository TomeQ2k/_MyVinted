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
    public class SendChangeEmailCallbackCommand : IRequestHandler<SendChangeEmailCallbackRequest, SendChangeEmailCallbackResponse>
    {
        private readonly IAccountManager accountManager;
        private readonly IAuthValidationService authValidationService;

        public SendChangeEmailCallbackCommand(IAccountManager accountManager, IAuthValidationService authValidationService)
        {
            this.accountManager = accountManager;
            this.authValidationService = authValidationService;
        }

        public async Task<SendChangeEmailCallbackResponse> Handle(SendChangeEmailCallbackRequest request, CancellationToken cancellationToken)
        {
            if (await authValidationService.EmailExists(request.NewEmail))
                throw new DuplicateException("Email address already exists", ErrorCodes.EmailExists);

            var changeEmailCallbackSent = await accountManager.SendChangeEmailCallback(request.NewEmail);

            return changeEmailCallbackSent ? new SendChangeEmailCallbackResponse()
                : throw new CannotGenerateTokenException("Generating change email token failed");
        }
    }
}