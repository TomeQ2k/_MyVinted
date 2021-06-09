using System.Collections.Generic;
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
    public class GetUserOrdersQuery : IRequestHandler<GetUserOrdersRequest, GetUserOrdersResponse>
    {
        private readonly IReadOnlyOrderService orderService;
        private readonly IMapper mapper;

        public GetUserOrdersQuery(IReadOnlyOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        public async Task<GetUserOrdersResponse> Handle(GetUserOrdersRequest request, CancellationToken cancellationToken)
        {
            var orders = await orderService.GetUserOrders(request);

            return new GetUserOrdersResponse { Orders = mapper.Map<List<OrderDto>>(orders) };
        }
    }
}