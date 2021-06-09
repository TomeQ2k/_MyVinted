using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class CartOperationException : ApplicationException
    {
        public string ErrorCode { get; }

        public CartOperationException(string message, string errorCode = ErrorCodes.CartOperationFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}