using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class ServerException : ApplicationException
    {
        public string ErrorCode { get; }

        public ServerException(string message, string errorCode = ErrorCodes.ServerError) : base(message)
            => (ErrorCode) = (errorCode);
    }
}