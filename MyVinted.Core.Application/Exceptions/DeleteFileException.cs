using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class DeleteFileException : ApplicationException
    {
        public string ErrorCode { get; }

        public DeleteFileException(string message, string errorCode = ErrorCodes.DeleteFileFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}