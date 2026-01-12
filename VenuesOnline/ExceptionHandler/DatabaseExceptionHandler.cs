using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Exceptions;

namespace FamousVenues.ExceptionHandler
{
    internal sealed class DatabaseExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<DatabaseExceptionHandler> _logger;

        public DatabaseExceptionHandler(ILogger<DatabaseExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not DatabaseException databaseException)
            {
                return false;
            }

            _logger.LogError(
                databaseException,
                "Exception occurred: {Message}",
                databaseException.Message);

            var problemDetails = new ProblemDetails
            {
                Status = (int)ErrorCode.DataBaseException,
                Title = "Occurred exception when get data in database",
                Detail = databaseException.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
