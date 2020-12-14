using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Order.Command.API.Core
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = new ErrorDetails()
            {
                Message = exception.Message,
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            context.Response.StatusCode = result.StatusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }

    internal class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
