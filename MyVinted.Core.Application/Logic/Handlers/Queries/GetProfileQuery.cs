using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
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