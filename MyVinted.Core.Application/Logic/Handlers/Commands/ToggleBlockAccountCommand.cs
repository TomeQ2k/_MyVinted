using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class ToggleBlockAccountCommand : IRequestHandler<ToggleBlockAccountRequest, ToggleBlockAccountResponse>
    {
        private readonly IUserManager userManager;
        private readonly IHttpContextReader httpContextReader;

        public ToggleBlockAccountCommand(IUserManager userManager, IHttpContextReader httpContextReader)
        {
            this.userManager = userManager;
            this.httpContextReader = httpContextReader;
        }

        public async Task<ToggleBlockAccountResponse> Handle(ToggleBlockAccountRequest request, CancellationToken cancellationToken)
        {
            var blockResult = await userManager.ToggleBlockAccount(request.UserId);

            return blockResult.IsSucceeded
                ? new ToggleBlockAccountResponse { IsBlocked = blockResult.IsBlocked }
                : throw new CrudException("Toggling block account status failed");
        }
    }
}