using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class NoPermissionsException : ApplicationException
    {
        public string ErrorCode { get; }

        public NoPermissionsException(string message, string errorCode = ErrorCodes.AccessDenied) : base(message)
            => (ErrorCode) = (errorCode);
    }
}