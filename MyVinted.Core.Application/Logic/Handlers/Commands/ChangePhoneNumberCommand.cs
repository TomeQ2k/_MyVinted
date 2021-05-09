using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class ChangePhoneNumberCommand : IRequestHandler<ChangePhoneNumberRequest, ChangePhoneNumberResponse>
    {
        private readonly IAccountManager accountManager;

        public ChangePhoneNumberCommand(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public async Task<ChangePhoneNumberResponse> Handle(ChangePhoneNumberRequest request, CancellationToken cancellationToken)
            => await accountManager.ChangePhoneNumber(request.NewPhoneNumber)
                ? new ChangePhoneNumberResponse()
                : throw new ProfileUpdateException("Changing phone number failed");
    }
}