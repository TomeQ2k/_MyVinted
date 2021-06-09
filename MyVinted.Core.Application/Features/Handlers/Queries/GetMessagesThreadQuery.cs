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
    public class GetMessagesThreadQuery : IRequestHandler<GetMessagesThreadRequest, GetMessagesThreadResponse>
    {
        private readonly IReadOnlyMessenger messenger;
        private readonly IReadOnlyUserService userService;
        private readonly IMapper mapper;
        private readonly IHttpContextWriter httpContextWriter;

        public GetMessagesThreadQuery(IReadOnlyMessenger messenger, IReadOnlyUserService userService, IMapper mapper,
            IHttpContextWriter httpContextWriter)
        {
            this.messenger = messenger;
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextWriter = httpContextWriter;
        }

        public async Task<GetMessagesThreadResponse> Handle(GetMessagesThreadRequest request,
            CancellationToken cancellationToken)
        {
            var messages = await messenger.GetMessagesThread(request.RecipientId, request);

            httpContextWriter.AddPagination(messages.CurrentPage, messages.PageSize, messages.TotalCount,
                messages.TotalPages);

            return new GetMessagesThreadResponse
            {
                Messages = mapper.Map<List<MessageDto>>(messages),
                Recipient = mapper.Map<RecipientDto>(await userService.GetUser(request.RecipientId))
            };
        }
    }
}