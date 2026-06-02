using CaseManagement.Application.Common.Exceptions;


namespace CaseManagement.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ConflictException ex)
            {
                _logger.LogWarning(ex, "Business rule violation - conflict");
                await WriteErrorResponseAsync(context, StatusCodes.Status409Conflict, ex.Message);

            }
            catch (RequestValidationException ex)
            {
                _logger.LogWarning(ex, "Valdation failed");
                await WriteErrorResponseAsync(context, StatusCodes.Status409Conflict, ex.Message);

            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found");
                await WriteErrorResponseAsync(context, StatusCodes.Status409Conflict, ex.Message);

            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized request.");
                await WriteErrorResponseAsync(context, StatusCodes.Status409Conflict, ex.Message);

            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "unexpected server error.");
                await WriteErrorResponseAsync(context, StatusCodes.Status409Conflict, ex.Message);
            }

        }

        private static async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new { statusCode, message });


        }
    }
}
