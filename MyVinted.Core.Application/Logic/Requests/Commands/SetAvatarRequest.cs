using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Validators;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Logic.Requests.Commands
{
    public record SetAvatarRequest : IRequest<SetAvatarResponse>
    {
        public IFormFile Avatar { get; init; }
    }

    public class SetAvatarRequestValidator : AbstractValidator<SetAvatarRequest>
    {
        public SetAvatarRequestValidator()
        {
            RuleFor(x => x.Avatar).NotNull().MaxFileSizeIs(Constants.MaxFileSize).AllowedFileExtensionsAre(isCollection: false, ".img", ".png", ".jpg", ".jpeg", ".tiff", ".ico", ".svg");
        }
    }
}