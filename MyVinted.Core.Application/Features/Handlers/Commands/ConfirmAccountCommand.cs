using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class ConfirmAccountCommand : IRequestHandler<ConfirmAccountRequest, ConfirmAccountResponse>
    {
        private readonly IIdentityService identityService;

        public ConfirmAccountCommand(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request, CancellationToken cancellationToken)
            => await identityService.ConfirmAccount(request.Email, request.Token)
                ? new ConfirmAccountResponse()
                : throw new AccountNotConfirmedException("Error occurred during confirmation account");
    }
}