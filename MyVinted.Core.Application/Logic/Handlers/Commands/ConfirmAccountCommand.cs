using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
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