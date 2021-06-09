using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Requests.Queries;

namespace MyVinted.API.AppConfigs
{
    public static class FluentValidationAppConfig
    {
        public static IMvcBuilder ConfigureFluentValidation(this IMvcBuilder mvcBuilder)
            => mvcBuilder.AddFluentValidation(v =>
            {
                v.RegisterValidatorsFromAssemblyContaining<SignInRequest>();
                v.RegisterValidatorsFromAssemblyContaining<SignInWithExternalProviderRequest>();
                v.RegisterValidatorsFromAssemblyContaining<SignUpRequest>();
                v.RegisterValidatorsFromAssemblyContaining<ConfirmAccountRequest>();
                v.RegisterValidatorsFromAssemblyContaining<ResetPasswordRequest>();
                v.RegisterValidatorsFromAssemblyContaining<SendResetPasswordCallbackRequest>();

                v.RegisterValidatorsFromAssemblyContaining<GetOfferRequest>();
                v.RegisterValidatorsFromAssemblyContaining<GetOfferToUpdateRequest>();
                v.RegisterValidatorsFromAssemblyContaining<AddOfferRequest>();
                v.RegisterValidatorsFromAssemblyContaining<UpdateOfferRequest>();
                v.RegisterValidatorsFromAssemblyContaining<DeleteOfferRequest>();
                v.RegisterValidatorsFromAssemblyContaining<LikeOfferRequest>();

                v.RegisterValidatorsFromAssemblyContaining<GetUserRequest>();
                v.RegisterValidatorsFromAssemblyContaining<ChangePasswordRequest>();
                v.RegisterValidatorsFromAssemblyContaining<ChangeEmailRequest>();
                v.RegisterValidatorsFromAssemblyContaining<ChangeUsernameRequest>();
                v.RegisterValidatorsFromAssemblyContaining<ChangePhoneNumberRequest>();
                v.RegisterValidatorsFromAssemblyContaining<SetAvatarRequest>();
                v.RegisterValidatorsFromAssemblyContaining<SendChangeEmailCallbackRequest>();
                v.RegisterValidatorsFromAssemblyContaining<FollowUserRequest>();

                v.RegisterValidatorsFromAssemblyContaining<AddOpinionRequest>();
                v.RegisterValidatorsFromAssemblyContaining<DeleteOpinionRequest>();

                v.RegisterValidatorsFromAssemblyContaining<MarkAsReadNotificationRequest>();
                v.RegisterValidatorsFromAssemblyContaining<RemoveNotificationRequest>();

                v.RegisterValidatorsFromAssemblyContaining<GetMessagesThreadRequest>();
                v.RegisterValidatorsFromAssemblyContaining<SendMessageRequest>();
                v.RegisterValidatorsFromAssemblyContaining<LikeMessageRequest>();
                v.RegisterValidatorsFromAssemblyContaining<DeleteMessageRequest>();
                v.RegisterValidatorsFromAssemblyContaining<ReadMessageRequest>();

                v.RegisterValidatorsFromAssemblyContaining<CreateOfferAuctionRequest>();
                v.RegisterValidatorsFromAssemblyContaining<AcceptOfferAuctionRequest>();
                v.RegisterValidatorsFromAssemblyContaining<DenyOfferAuctionRequest>();

                v.RegisterValidatorsFromAssemblyContaining<AddToCartRequest>();
                v.RegisterValidatorsFromAssemblyContaining<RemoveCartItemRequest>();

                v.RegisterValidatorsFromAssemblyContaining<PurchaseOrderRequest>();

                v.RegisterValidatorsFromAssemblyContaining<DeleteOfferPhotoRequest>();

                v.RegisterValidatorsFromAssemblyContaining<StartConnectionRequest>();
                v.RegisterValidatorsFromAssemblyContaining<CloseConnectionRequest>();

                v.RegisterValidatorsFromAssemblyContaining<GetLogsRequest>();

                v.RegisterValidatorsFromAssemblyContaining<ToggleBlockAccountRequest>();

                v.ImplicitlyValidateRootCollectionElements = true;
            });
    }
}