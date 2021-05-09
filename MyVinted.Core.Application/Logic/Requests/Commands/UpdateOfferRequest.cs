using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Validators;
using MyVinted.Core.Common.Helpers;
using System.Collections.Generic;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record UpdateOfferRequest : IRequest<UpdateOfferResponse>
    {
        public string OfferId { get; init; }
        public string Title { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; }
        public bool AllowBidding { get; init; }
        public string CategoryId { get; init; }

        public IEnumerable<IFormFile> Photos { get; init; }
    }

    public class UpdateOfferRequestValidator : AbstractValidator<UpdateOfferRequest>
    {
        public UpdateOfferRequestValidator()
        {
            RuleFor(x => x.OfferId).NotNull();
            RuleFor(x => x.Title).NotNull().MaximumLength(Constants.MaxTitleLength);
            RuleFor(x => x.Price).NotNull().InclusiveBetween(1, Constants.MaxPrice);
            RuleFor(x => x.Description).NotNull().MaximumLength(Constants.MaxDescriptionLength);
            RuleFor(x => x.AllowBidding).NotNull();
            RuleFor(x => x.CategoryId).NotNull();
            RuleForEach(x => x.Photos).NotNull().MaxFileSizeIs(Constants.MaxFileSize)
                .AllowedFileExtensionsAre(isCollection: true, ".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg");
        }
    }
}