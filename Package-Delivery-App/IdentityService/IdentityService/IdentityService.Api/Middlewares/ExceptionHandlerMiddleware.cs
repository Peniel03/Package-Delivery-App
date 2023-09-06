using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Interfaces;

namespace IdentityService.Api.Middlewares
{
    /// <summary>
    /// Class to handle the exception middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly  ILoggerManager _logger;

        /// <summary>
        /// Initializes a instance of <see cref="ExceptionHandlerMiddleware"/>
        /// </summary>
        /// <param name="next">A <see cref="RequestDelegate"/> that point toward the next middleware</param>
        /// <param name="logger">the logger</param>
        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Handler of exceptions.
        /// </summary>
        /// <param name="context">Http context</param>
         public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = $"Error occured: {ex.Message}{Environment.NewLine}{ex.StackTrace}";

                _logger.LogError($"{message}");

                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Function to handle the exception
        /// </summary>
        /// <param name="httpContext">Http context.</param>
        /// <param name="exception">Current exception.</param>
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = GetCodeStatus(exception);

            await httpContext.Response
                .WriteAsync($"{httpContext.Response.StatusCode}\n Message : {exception.Message}");
        }

        /// <summary>
        /// Function to get the status of the exception
        /// </summary>
        /// <param name="exception">Current exception.</param>
        /// <returns></returns>
        private static int GetCodeStatus(Exception exception)
        {
            if (exception is NotFoundException)
            {
                return StatusCodes.Status400BadRequest;
            }
            else if (exception is AlreadyExistException)
            {
                return StatusCodes.Status403Forbidden;
            }

            return StatusCodes.Status500InternalServerError;
        }

    }
}
