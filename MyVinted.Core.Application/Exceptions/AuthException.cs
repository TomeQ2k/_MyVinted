using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class AuthException : ApplicationException
    {
        public string ErrorCode { get; }

        public AuthException(string message, string errorCode = ErrorCodes.AuthError) : base(message)
            => (ErrorCode) = (errorCode);
    }
}