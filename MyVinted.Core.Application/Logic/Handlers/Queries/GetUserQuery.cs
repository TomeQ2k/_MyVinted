using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
{
    public class GetUserQuery : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IReadOnlyUserService userService;
        private readonly IMapper mapper;

        public GetUserQuery(IReadOnlyUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUser(request.UserId);

            return user != null ? new GetUserResponse { User = mapper.Map<UserDto>(user) }
                : throw new EntityNotFoundException("User not found");
        }
    }
}