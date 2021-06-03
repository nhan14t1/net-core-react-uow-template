using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using R.Entities.Exceptions;
using R.Entities.Entities.Others;

namespace R.WebUI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var baseEx = ex.GetBaseException();
            if (!(ex is AppException))
            {
                return;
            }
            //
            // Convert to model
            var error = new ErrorModel("Sorry, an error has occurred");
            if (ex is AppException)
            {
                error.Message = baseEx.Message;
                //error.Code = (ex as AppException).ErrorCode;
            }

            _logger.LogError(baseEx, baseEx.Message);
            //
            // Return as json
            context.Response.ContentType = "application/json";
            using (var writer = new StreamWriter(context.Response.Body))
            {
                new CamelCaseJsonSerializer().Serialize(writer, error);
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }
    }
}
