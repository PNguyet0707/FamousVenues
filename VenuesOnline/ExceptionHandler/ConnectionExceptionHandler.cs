using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Exceptions;

namespace FamousVenues.ExceptionHandler
{
    internal sealed class ConnectionExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ConnectionExceptionHandler> _logger;

        public ConnectionExceptionHandler(ILogger<ConnectionExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not ConnectionException connectionException)
            {
                return false;
            }

            _logger.LogError(
                connectionException,
                "Exception occurred: {Message}",
                connectionException.Message);

            var problemDetails = new ProblemDetails
            {
                Status = (int)ErrorCode.ConnectionException,
                Title = "Occurred exception when connect to database",
                Detail = connectionException.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}