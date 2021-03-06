using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class InvalidCredentialsException : ApplicationException
    {
        public string ErrorCode { get; }

        public InvalidCredentialsException(string message, string errorCode = ErrorCodes.InvalidCredentials) : base(message)
            => (ErrorCode) = (errorCode);
    }
}