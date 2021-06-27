using System.Threading.Tasks;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Results;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Common.Helpers;
using MyVinted.Core.Domain.Data;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRolesManager rolesManager;

        public UserManager(IUnitOfWork unitOfWork, IRolesManager rolesManager)
        {
            this.unitOfWork = unitOfWork;
            this.rolesManager = rolesManager;
        }

        public async Task<BlockAccountResult> ToggleBlockAccount(string userId)
        {
            var user = await unitOfWork.UserRepository.FindById(userId) ??
                       throw new EntityNotFoundException("User not found");

            if (rolesManager.IsPermitted(RoleName.Admin, user))
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            user.ToggleIsBlocked();

            unitOfWork.UserRepository.Update(user);

            return new BlockAccountResult(await unitOfWork.Complete(), user.IsBlocked);
        }
    }
}