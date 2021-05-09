using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Responses.Queries;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;

namespace MyVinted.Core.Application.Logic.Handlers.Queries
{
    public class GetAllOrdersQuery : IRequestHandler<GetAllOrdersRequest, GetAllOrdersResponse>
    {
        private readonly IReadOnlyOrderService orderService;
        private readonly IHttpContextWriter httpContextWriter;
        private readonly IMapper mapper;

        public GetAllOrdersQuery(IReadOnlyOrderService orderService, IHttpContextWriter httpContextWriter, IMapper mapper)
        {
            this.orderService = orderService;
            this.httpContextWriter = httpContextWriter;
            this.mapper = mapper;
        }

        public async Task<GetAllOrdersResponse> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
        {
            var orders = await orderService.GetAllOrders(request);

            httpContextWriter.AddPagination(orders.CurrentPage, orders.PageSize, orders.TotalCount, orders.TotalPages);

            return new GetAllOrdersResponse { Orders = mapper.Map<IEnumerable<OrderAdminDto>>(orders) };
        }
    }
}