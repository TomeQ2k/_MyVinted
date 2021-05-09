using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyVinted.Core.Application.Dtos;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
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