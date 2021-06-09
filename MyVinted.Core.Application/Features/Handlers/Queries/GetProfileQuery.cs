using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;

namespace MyVinted.Core.Application.Features.Handlers.Queries
{
    public class GetProfileQuery : IRequestHandler<GetProfileRequest, GetProfileResponse>
    {
        private readonly IReadOnlyAccountManager accountManager;
        private readonly IMapper mapper;

        public GetProfileQuery(IReadOnlyAccountManager accountManager, IMapper mapper)
        {
            this.accountManager = accountManager;
            this.mapper = mapper;
        }

        public async Task<GetProfileResponse> Handle(GetProfileRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await accountManager.GetCurrentUser();

            return new GetProfileResponse { UserProfile = mapper.Map<UserProfileDto>(currentUser) };
        }
    }
}