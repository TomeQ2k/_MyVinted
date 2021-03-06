using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class ChangePasswordException : ApplicationException
    {
        public string ErrorCode { get; }

        public ChangePasswordException(string message, string errorCode = ErrorCodes.ChangePasswordFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}