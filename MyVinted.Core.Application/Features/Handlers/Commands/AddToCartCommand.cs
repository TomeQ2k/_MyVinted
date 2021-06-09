using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Enums;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class AddToCartCommand : IRequestHandler<AddToCartRequest, AddToCartResponse>
    {
        private readonly ICartManager cartManager;
        private readonly IBookingService bookingService;
        private readonly IMapper mapper;

        public AddToCartCommand(ICartManager cartManager, IBookingService bookingService, IMapper mapper)
        {
            this.cartManager = cartManager;
            this.bookingService = bookingService;
            this.mapper = mapper;
        }

        public async Task<AddToCartResponse> Handle(AddToCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await cartManager.GetCart();

            var response = request.Type switch
            {
                OrderType.Offer => !await bookingService.BookOffer(request.OfferId)
                    ? throw new CartOperationException("Booking offer failed") : new AddToCartResponse(),
                OrderType.Premium when cart != null => cart.Items.Any(i => i.Type == OrderType.Premium)
                    ? throw new CartOperationException("You already have upgrade account in your cart") : new AddToCartResponse(),
                _ => new AddToCartResponse()
            };

            return response.IsSucceeded ? response with { CartItem = mapper.Map<OrderItemDto>(await cartManager.AddItem(request)) } : response;
        }
    }
}