using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
{
    public class SignInWithExternalProviderCommand : IRequestHandler<SignInWithExternalProviderRequest, SignInWithExternalProviderResponse>
    {
        private readonly IServiceProvider services;
        private readonly IMapper mapper;

        public SignInWithExternalProviderCommand(IServiceProvider services, IMapper mapper)
        {
            this.services = services;
            this.mapper = mapper;
        }

        public async Task<SignInWithExternalProviderResponse> Handle(SignInWithExternalProviderRequest request, CancellationToken cancellationToken)
        {
            IExternalIdentityService identityService = request.Provider switch
            {
                Constants.GoogleProvider => (IExternalIdentityService)services.GetRequiredService<IGoogleIdentityService>(),
                Constants.FacebookProvider => (IExternalIdentityService)services.GetRequiredService<IFacebookIdentityService>(),
                _ => null
            } ?? throw new ExternalAuthException("Invalid external login provider");

            var identityResult = await identityService.SignInWithExternalProvider(request.Provider, request.IdToken);

            if (identityResult != null)
                return new SignInWithExternalProviderResponse { Token = identityResult.Token, User = mapper.Map<UserAuthDto>(identityResult.User) };

            throw new ExternalAuthException("Error occurred during external authentication");
        }
    }
}