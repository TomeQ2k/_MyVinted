using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Features.Requests.Queries;
using MyVinted.Core.Application.Features.Responses.Queries;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;

namespace MyVinted.Core.Application.Features.Handlers.Queries
{
    public class GetUsersQuery : IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IReadOnlyUserService userService;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public GetUsersQuery(IReadOnlyUserService userService, IMapper mapper, IHttpContextService httpContextService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await userService.GetUsers(request with {CurrentUserId = httpContextService.CurrentUserId});

            var usersToReturn = mapper.Map<List<UserListDto>>(users);

            httpContextService.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return new GetUsersResponse {Users = usersToReturn};
        }
    }
}