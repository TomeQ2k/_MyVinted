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