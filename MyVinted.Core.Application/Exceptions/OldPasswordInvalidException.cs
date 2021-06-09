using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class OldPasswordInvalidException : ApplicationException
    {
        public string ErrorCode { get; }

        public OldPasswordInvalidException(string message, string errorCode = ErrorCodes.OldPasswordInvalid) : base(message)
            => (ErrorCode) = (errorCode);
    }
}