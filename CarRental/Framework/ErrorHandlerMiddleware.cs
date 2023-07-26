using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace CarRental.API.Framework
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                await HandlerErrorAsync(context, e);
            }
        }

        private static Task HandlerErrorAsync(HttpContext context, Exception exception) 
        {
            var exceptionType = exception.GetType();
            var statusCode = HttpStatusCode.InternalServerError;
            switch (exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException) :
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case Exception e when exceptionType == typeof(ArgumentException) :
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }
            var response = new { message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}
