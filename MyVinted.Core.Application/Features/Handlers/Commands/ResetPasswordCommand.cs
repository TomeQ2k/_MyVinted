using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class ResetPasswordCommand : IRequestHandler<ResetPasswordRequest, ResetPasswordResponse>
    {
        private readonly IResetPasswordManager resetPasswordManager;

        public ResetPasswordCommand(IResetPasswordManager resetPasswordManager)
        {
            this.resetPasswordManager = resetPasswordManager;
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
            => await resetPasswordManager.ResetPassword(request.Email, request.NewPassword, request.Token)
                ? new ResetPasswordResponse()
                : throw new ResetPasswordException("Error occurred during resetting password");
    }
}