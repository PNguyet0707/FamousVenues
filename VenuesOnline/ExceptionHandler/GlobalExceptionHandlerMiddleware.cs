using Microsoft.AspNetCore.Mvc;

namespace FamousVenues.ExceptionHandler
{
    internal sealed class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occurred");

                // Make sure to set the status code before writing to the response body
                context.Response.StatusCode = ex switch
                {
                    ApplicationException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                await context.Response.WriteAsJsonAsync(
                    new ProblemDetails
                    {
                        Type = ex.GetType().Name,
                        Title = "An error occured",
                        Detail = ex.Message
                    });
            }
        }
    }
}
