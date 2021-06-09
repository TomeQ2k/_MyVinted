using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class CrudException : ApplicationException
    {
        public string ErrorCode { get; }

        public CrudException(string message, string errorCode = ErrorCodes.CrudActionFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}