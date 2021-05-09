using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Common.Helpers;
using System.Security.Cryptography;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly IDataProtectionProvider dataProtectionProvider;

        public string ProtectorToken { get; init; }

        public IConfiguration Configuration { get; init; }

        public CryptoService(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            this.dataProtectionProvider = dataProtectionProvider;

            Configuration = configuration;
            ProtectorToken = Configuration.GetValue<string>(AppSettingsKeys.TLSToken);
        }

        public string Encrypt(string plainText)
        {
            var dataProtector = dataProtectionProvider.CreateProtector(ProtectorToken);

            return dataProtector.Protect(plainText);
        }

        public string Decrypt(string cipherText)
        {
            try
            {
                var dataProtector = dataProtectionProvider.CreateProtector(ProtectorToken);

                return dataProtector.Unprotect(cipherText);
            }
            catch (CryptographicException)
            {
                throw new ServerException("Error occurred during decrypting");
            }
        }
    }
}