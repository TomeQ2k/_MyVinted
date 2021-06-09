using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class RemoveCartItemCommand : IRequestHandler<RemoveCartItemRequest, RemoveCartItemResponse>
    {
        private readonly ICartManager cartManager;

        public RemoveCartItemCommand(ICartManager cartManager)
        {
            this.cartManager = cartManager;
        }

        public async Task<RemoveCartItemResponse> Handle(RemoveCartItemRequest request, CancellationToken cancellationToken)
            => await cartManager.RemoveItem(request.ItemId) ? new RemoveCartItemResponse()
                : throw new CartOperationException("Removing item from cart failed");
    }
}