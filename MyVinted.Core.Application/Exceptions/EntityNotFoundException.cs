using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public string ErrorCode { get; }

        public EntityNotFoundException(string message, string errorCode = ErrorCodes.EntityNotFound) : base(message)
            => (ErrorCode) = (errorCode);
    }
}