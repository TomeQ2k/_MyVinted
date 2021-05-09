using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
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