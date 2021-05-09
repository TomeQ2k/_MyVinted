using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class ExternalAuthException : Exception
    {
        public string ErrorCode { get; }

        public ExternalAuthException(string message, string errorCode = ErrorCodes.ExternalAuthError) : base(message)
            => (ErrorCode) = (errorCode);
    }
}