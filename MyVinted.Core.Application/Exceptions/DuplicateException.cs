using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class DuplicateException : ApplicationException
    {
        public string ErrorCode { get; }

        public DuplicateException(string message, string errorCode = ErrorCodes.DuplicateExists) : base(message)
            => (ErrorCode) = (errorCode);
    }
}