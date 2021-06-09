using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class PaymentException : ApplicationException
    {
        public string ErrorCode { get; }

        public PaymentException(string message, string errorCode = ErrorCodes.PaymentError) : base(message)
            => (ErrorCode) = (errorCode);
    }
}