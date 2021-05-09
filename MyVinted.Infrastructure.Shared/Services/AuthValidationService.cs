using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using System.Threading.Tasks;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class AuthValidationService : BaseValidationService, IAuthValidationService
    {
        public AuthValidationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<bool> EmailExists(string email)
            => await unitOfWork.UserRepository.Find(u => u.NormalizedEmail == email.ToUpper()) != null;

        public async Task<bool> UsernameExists(string username)
            => await unitOfWork.UserRepository.Find(u => u.NormalizedUserName == username.ToUpper()) != null;
    }
}