using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class CannotGenerateTokenException : Exception
    {
        public string ErrorCode { get; }

        public CannotGenerateTokenException(string message, string errorCode = ErrorCodes.CannotGenerateToken) : base(message)
            => (ErrorCode) = (errorCode);
    }
}