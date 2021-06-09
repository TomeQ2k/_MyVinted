using MyVinted.Core.Common.Helpers;
using System;

namespace MyVinted.Core.Application.Exceptions
{
    public class UploadFileException : ApplicationException
    {
        public string ErrorCode { get; }

        public UploadFileException(string message, string errorCode = ErrorCodes.UploadFileFailed) : base(message)
            => (ErrorCode) = (errorCode);
    }
}