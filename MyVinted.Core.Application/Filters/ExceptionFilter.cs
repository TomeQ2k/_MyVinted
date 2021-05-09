using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Logic.Responses;
using MyVinted.Core.Common.Helpers;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var errorMessage = context.Exception.Message;
            var (statusCode, errorCode) = (HttpStatusCode.InternalServerError, ErrorCodes.ServerError);

            (statusCode, errorCode) = context.Exception switch
            {
                ServerException _ or CrudException _ or ResetPasswordException _ or
                    CannotGenerateTokenException _ or ProfileUpdateException _ or ChangePasswordException _ or
                    UploadFileException _ or DeleteFileException _ or CartOperationException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),

                EntityNotFoundException _ => (HttpStatusCode.NotFound, GetErrorCode(context.Exception)),

                AuthException _ or ExternalAuthException _ or InvalidCredentialsException _ or
                    AccountNotConfirmedException _ => (HttpStatusCode.Unauthorized, GetErrorCode(context.Exception)),

                ServiceException _ => (HttpStatusCode.ServiceUnavailable, GetErrorCode(context.Exception)),

                NoPermissionsException _ or BlockException _ => (HttpStatusCode.Forbidden, GetErrorCode(context.Exception)),

                DuplicateException _ => (HttpStatusCode.Conflict, GetErrorCode(context.Exception)),

                PaymentException _ => (HttpStatusCode.PaymentRequired, GetErrorCode(context.Exception)),

                OldPasswordInvalidException _ => (HttpStatusCode.BadRequest, GetErrorCode(context.Exception)),

                _ => (HttpStatusCode.InternalServerError, ErrorCodes.ServerError)
            };

            var jsonResponse = (new BaseResponse(Error.Build(errorCode, errorMessage, statusCode: statusCode))).ToJSON();

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.HttpContext.Response.ContentLength = Encoding.UTF8.GetBytes(jsonResponse).Length;

            context.HttpContext.Response.AddApplicationError(errorMessage);

            await context.HttpContext.Response.WriteAsync(jsonResponse);

            await base.OnExceptionAsync(context);
        }

        #region private

        private string GetErrorCode(Exception exception) => (string)exception.GetType().GetProperty("ErrorCode").GetValue(exception, null);

        #endregion
    }
}