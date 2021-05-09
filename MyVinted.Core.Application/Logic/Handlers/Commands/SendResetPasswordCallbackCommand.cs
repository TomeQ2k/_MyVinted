using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class SendResetPasswordCallbackCommand : IRequestHandler<SendResetPasswordCallbackRequest, SendResetPasswordCallbackResponse>
    {
        private readonly IResetPasswordManager resetPasswordManager;

        public SendResetPasswordCallbackCommand(IResetPasswordManager resetPasswordManager)
        {
            this.resetPasswordManager = resetPasswordManager;
        }

        public async Task<SendResetPasswordCallbackResponse> Handle(SendResetPasswordCallbackRequest request, CancellationToken cancellationToken)
            => await resetPasswordManager.SendResetPasswordCallback(request.Email, request.NewPassword)
                ? new SendResetPasswordCallbackResponse()
                : throw new CannotGenerateTokenException("Generating reset password token failed");
    }
}