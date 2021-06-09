using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class AccountNotConfirmedException : ApplicationException
    {
        public string ErrorCode { get; }

        public AccountNotConfirmedException(string message, string errorCode = ErrorCodes.AccountNotConfirmed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}