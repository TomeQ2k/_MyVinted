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
    public class GetOrdersQuery : IRequestHandler<GetOrdersRequest, GetOrdersResponse>
    {
        private readonly IReadOnlyOrderService orderService;
        private readonly IHttpContextWriter httpContextWriter;
        private readonly IMapper mapper;

        public GetOrdersQuery(IReadOnlyOrderService orderService, IHttpContextWriter httpContextWriter, IMapper mapper)
        {
            this.orderService = orderService;
            this.httpContextWriter = httpContextWriter;
            this.mapper = mapper;
        }

        public async Task<GetOrdersResponse> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
        {
            var orders = await orderService.GetOrders(request);

            httpContextWriter.AddPagination(orders.CurrentPage, orders.PageSize, orders.TotalCount, orders.TotalPages);

            return new GetOrdersResponse { Orders = mapper.Map<IEnumerable<OrderAdminDto>>(orders) };
        }
    }
}