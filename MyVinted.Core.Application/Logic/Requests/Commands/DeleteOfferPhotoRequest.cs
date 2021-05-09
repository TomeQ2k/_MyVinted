using FluentValidation;
using MediatR;
using MyVinted.Core.Application.Logic.Responses.Commands;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record DeleteOfferPhotoRequest : IRequest<DeleteOfferPhotoResponse>
    {
        public string PhotoId { get; init; }
        public string OfferId { get; init; }
    }

    public class DeleteOfferPhotoRequestValidator : AbstractValidator<DeleteOfferPhotoRequest>
    {
        public DeleteOfferPhotoRequestValidator()
        {
            RuleFor(x => x.PhotoId).NotNull();
            RuleFor(x => x.OfferId).NotNull();
        }
    }
}