using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
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