using ShipmentService.BusinessLogic.Exceptions;
using ShipmentService.BusinessLogic.Interfaces;

namespace ShipmentService.Api.Middlewares
{
    /// <summary>
    /// the exception handler middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private  readonly ILoggerManager _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ExceptionHandlerMiddleware"/>
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Handler for exceptions.
        /// </summary>
        /// <param name="context">Http context.</param>
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
        ///  Function to handle the exception
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
        private static int GetCodeStatus(Exception exception)
        {
            if (exception is NotFoundException)
            {
                return StatusCodes.Status400BadRequest;
            }
            else if (exception is AlreadyExistException)
            {
                return StatusCodes.Status400BadRequest;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            } 
        }
    }
}
