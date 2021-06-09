using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class ServiceException : ApplicationException
    {
        public string ErrorCode { get; }

        public ServiceException(string message, string errorCode = ErrorCodes.ServiceError) : base(message)
            => (ErrorCode) = (errorCode);
    }
}