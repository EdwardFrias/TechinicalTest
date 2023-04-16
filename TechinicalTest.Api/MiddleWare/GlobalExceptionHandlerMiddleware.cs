using System.Net;
using System.Text.Json;
using TechnicalTest.Core.AppExceptions;
using TechnicalTest.Core.Responses;

namespace TechinicalTest.Api.MiddleWare
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {

                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Response.ContentType = "application/json";

                string json = JsonSerializer.Serialize(ApiResponse<string>.Fail(e.Message));

                if (e is AppException)
                {
                    json = JsonSerializer.Serialize(
                        ApiResponse<AppException>.Fail(((AppException)e).Errors.ToArray(), e.Message)
                        );
                    context.Response.StatusCode = ((AppException)e).Code;
                }

                await context.Response.WriteAsync(json);

            }
        }
    }
}
