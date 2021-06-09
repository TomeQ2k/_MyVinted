using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class ResetPasswordException : ApplicationException
    {
        public string ErrorCode { get; }

        public ResetPasswordException(string message, string errorCode = ErrorCodes.ResetPasswordFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}