using AutoMapper;
using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Requests.Queries;
using MyVinted.Core.Application.Logic.Requests.Queries.Params;
using MyVinted.Core.Application.ServiceUtils;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(u => u.IsVerified()))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(u => u.IsAdmin()))
                .ForMember(dest => dest.FollowsCount, opt => opt.MapFrom(u => u.Followings.Count))
                .ForMember(dest => dest.OpinionsCount, opt => opt.MapFrom(u => u.Opinions.Count))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(u => RatingUtils.CalculateRating(u)))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(u => StorageLocation.BuildLocation(u.AvatarUrl)));
            CreateMap<User, UserListDto>()
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(u => u.IsVerified()))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(u => u.IsAdmin()))
                .ForMember(dest => dest.FollowsCount, opt => opt.MapFrom(u => u.Followings.Count))
                .ForMember(dest => dest.OpinionsCount, opt => opt.MapFrom(u => u.Opinions.Count))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(u => RatingUtils.CalculateRating(u)))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(u => StorageLocation.BuildLocation(u.AvatarUrl)));
            CreateMap<User, UserAuthDto>()
                .ForMember(dest => dest.IsExternalUser, opt => opt.MapFrom(u => u.IsExternal()))
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(u => u.IsVerified()))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(u => RatingUtils.CalculateRating(u)))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(u => StorageLocation.BuildLocation(u.AvatarUrl)));
            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.IsRegistered, opt => opt.MapFrom(u => u.IsRegistered()))
                .ForMember(dest => dest.IsExternalUser, opt => opt.MapFrom(u => u.IsExternal()))
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(u => u.IsVerified()))
                .ForMember(dest => dest.Balance,
                    opt => opt.MapFrom(u => u.BalanceAccount != null ? u.BalanceAccount.Balance : 0))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(u => StorageLocation.BuildLocation(u.AvatarUrl)));
            CreateMap<User, RecipientDto>()
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(u => StorageLocation.BuildLocation(u.AvatarUrl)));

            CreateMap<Offer, OfferDto>()
                .ForMember(dest => dest.FirstPhotoUrl, opt => opt.MapFrom(o => o.GetFirstPhotoUrl()))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(o => o.OfferLikes.Count))
                .ForMember(dest => dest.IsBooked, opt => opt.MapFrom(o => o.BookingUserId != null));
            CreateMap<Offer, OfferToUpdateDto>();
            CreateMap<Offer, OfferListDto>()
                .ForMember(dest => dest.FirstPhotoUrl, opt => opt.MapFrom(o => o.GetFirstPhotoUrl()))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(o => o.OfferLikes.Count))
                .ForMember(dest => dest.IsBooked, opt => opt.MapFrom(o => o.BookingUserId != null));
            CreateMap<UpdateOfferRequest, Offer>()
                .ForMember(dest => dest.OfferPhotos, opt => opt.Ignore());

            CreateMap<UserRole, UserRoleDto>();

            CreateMap<Category, CategoryDto>();

            CreateMap<OfferPhoto, OfferPhotoDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(op => StorageLocation.BuildLocation(op.Path)));

            CreateMap<OfferLike, OfferLikeDto>();

            CreateMap<UserFollow, UserFollowDto>();

            CreateMap<Opinion, OpinionDto>();

            CreateMap<Notification, NotificationDto>();

            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(m => m.Sender.UserName))
                .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(m => m.Recipient.UserName))
                .ForMember(dest => dest.SenderAvatarUrl,
                    opt => opt.MapFrom(m => StorageLocation.BuildLocation(m.Sender.AvatarUrl)))
                .ForMember(dest => dest.RecipientAvatarUrl,
                    opt => opt.MapFrom(m => StorageLocation.BuildLocation(m.Recipient.AvatarUrl)));

            CreateMap<OfferAuction, OfferAuctionDto>();

            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderAdminDto>();

            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Cart, CartDto>();

            CreateMap<GetOffersRequest, OfferFiltersParams>();
            CreateMap<GetFavoritesOffersRequest, OfferFiltersParams>();
            CreateMap<GetMyOffersRequest, OfferFiltersParams>();
        }
    }
}