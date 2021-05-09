using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class SignInCommand : IRequestHandler<SignInRequest, SignInResponse>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public SignInCommand(IIdentityService identityService, IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }

        public async Task<SignInResponse> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var identityResult = await identityService.SignIn(request.Email, request.Password);

            if (identityResult != null)
                return new SignInResponse { Token = identityResult.Token, User = mapper.Map<UserAuthDto>(identityResult.User) };

            throw new AuthException("Error occurred during signing in");
        }
    }
}