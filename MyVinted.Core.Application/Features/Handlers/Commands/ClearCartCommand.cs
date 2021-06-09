using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class ClearCartCommand : IRequestHandler<ClearCartRequest, ClearCartResponse>
    {
        private readonly ICartManager cartManager;

        public ClearCartCommand(ICartManager cartManager)
        {
            this.cartManager = cartManager;
        }

        public async Task<ClearCartResponse> Handle(ClearCartRequest request, CancellationToken cancellationToken)
            => await cartManager.ClearCart() ? new ClearCartResponse()
                : throw new CartOperationException("Clearing cart failed");
    }
}