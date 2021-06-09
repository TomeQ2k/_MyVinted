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
    public class GetCartQuery : IRequestHandler<GetCartRequest, GetCartResponse>
    {
        private readonly IReadOnlyCartManager cartManager;
        private readonly IMapper mapper;

        public GetCartQuery(IReadOnlyCartManager cartManager, IMapper mapper)
        {
            this.cartManager = cartManager;
            this.mapper = mapper;
        }

        public async Task<GetCartResponse> Handle(GetCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await cartManager.GetCart();

            return new GetCartResponse { Cart = mapper.Map<CartDto>(cart) };
        }
    }
}