using System.Net;
using CommonService.Dtos;
using Todo.Utilities.Exceptions;

namespace Todo.API.Middleware
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception processing {Method} {Path}", context.Request.Method, context.Request.Path);
                if (!context.Response.HasStarted)
                {
                    var (statusCode, message) = ex switch
                    {
                        NotFoundException => ((int)HttpStatusCode.NotFound, ex.Message),
                        ConflictException => ((int)HttpStatusCode.Conflict, ex.Message),
                        BadRequestException => ((int)HttpStatusCode.BadRequest, ex.Message),
                        UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, ex.Message),
                        _ => ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.")
                    };

                    var response = new OperationResponse(
                        hasSucceeded: false,
                        statusCode: statusCode,
                        message: message,
                        result: null
                    );

                    context.Response.Clear();
                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/json";

                    var json = System.Text.Json.JsonSerializer.Serialize(response, new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
                    });
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}


