using MyVinted.Core.Application.Builders;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System.Security.Authentication;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;
using MyVinted.Infrastructure.Shared.Specifications;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class OpinionService : IOpinionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReadOnlyAccountManager accountManager;
        private readonly IHttpContextReader httpContextReader;

        public OpinionService(IUnitOfWork unitOfWork, IReadOnlyAccountManager accountManager,
            IHttpContextReader httpContextReader)
        {
            this.unitOfWork = unitOfWork;
            this.accountManager = accountManager;
            this.httpContextReader = httpContextReader;
        }

        public async Task<Opinion> AddOpinion(string text, int rating, string userId)
        {
            var currentUser = await accountManager.GetCurrentUser();

            if (currentUser.Id == userId)
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            var user = await unitOfWork.UserRepository.FindById(userId) ??
                       throw new EntityNotFoundException("User not found");

            if (AddedOneOpinionPerUserSpecification.Create(currentUser.Id).IsSatisfied(user))
                throw new DuplicateException("You are allowed to add only one opinion per user");

            var opinion = new OpinionBuilder()
                .SetText(text)
                .SetRating(rating)
                .AboutUser(userId)
                .AddedBy(currentUser.Id)
                .Build();

            unitOfWork.OpinionRepository.Add(opinion);

            return await unitOfWork.Complete() ? opinion : null;
        }

        public async Task<bool> DeleteOpinion(string opinionId, string userId)
        {
            var currentUserId = httpContextReader.CurrentUserId ??
                                throw new AuthenticationException(ErrorMessages.NotAuthenticatedMessage);
            var opinion = await unitOfWork.OpinionRepository.Find(o =>
                              o.Id == opinionId && o.UserId == userId && o.CreatorId == currentUserId)
                          ?? throw new EntityNotFoundException("Opinion not found");

            unitOfWork.OpinionRepository.Delete(opinion);

            return await unitOfWork.Complete();
        }
    }
}