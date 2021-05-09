using System;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Application.Exceptions
{
    public class BlockException : Exception
    {
        public string ErrorCode { get; }

        public BlockException(string message = "Your account is blocked", string errorCode = ErrorCodes.AccountBlocked) : base(message)
            => (ErrorCode) = (errorCode);
    }
}