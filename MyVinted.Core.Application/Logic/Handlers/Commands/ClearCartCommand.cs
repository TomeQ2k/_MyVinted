using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
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