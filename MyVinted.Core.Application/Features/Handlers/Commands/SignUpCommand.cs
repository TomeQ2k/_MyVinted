using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Helpers;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class SignUpCommand : IRequestHandler<SignUpRequest, SignUpResponse>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        private readonly IAuthValidationService authValidationService;
        private readonly IEmailSender emailSender;
        private readonly IBalanceService balanceService;

        public IConfiguration Configuration { get; }

        public SignUpCommand(IIdentityService identityService, IMapper mapper, IAuthValidationService authValidationService, IEmailSender emailSender,
            IBalanceService balanceService, IConfiguration configuration)
        {
            this.identityService = identityService;
            this.mapper = mapper;
            this.authValidationService = authValidationService;
            this.emailSender = emailSender;
            this.balanceService = balanceService;

            Configuration = configuration;
        }

        public async Task<SignUpResponse> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            if (await authValidationService.EmailExists(request.Email))
                throw new DuplicateException("Email address already exists", ErrorCodes.EmailExists);

            if (await authValidationService.UsernameExists(request.Username))
                throw new DuplicateException("Username already exists", ErrorCodes.UsernameExists);

            var user = await identityService.SignUp(request.Email, request.Username, request.Password);

            if (user != null)
            {
                await balanceService.CreateBalanceAccount(user.Id);

                var confirmAccountToken = await identityService.GenerateConfirmAccountToken(user);

                string callbackUrl = $"{Configuration.GetValue<string>(AppSettingsKeys.ClientAddress)}register/confirm?email={user.Email}&token={confirmAccountToken}";

                return await emailSender.Send(EmailMessages.ActivationAccountEmail(user.Email, user.UserName, callbackUrl))
                    ? new SignUpResponse { Token = confirmAccountToken, User = mapper.Map<UserAuthDto>(user) }
                    : throw new ServiceException("Account confirmation email sending failed");
            }

            throw new AuthException("Error occurred during signing up");
        }
    }
}