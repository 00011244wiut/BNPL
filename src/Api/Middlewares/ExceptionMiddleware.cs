using System.Net;
using Api.Models;
using Application.Exceptions;
using FluentValidation;

namespace Api.Middlewares;

public class ExceptionMiddleware : Exception
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem;

        switch (ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    // Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException NotFound:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = NotFound.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = NotFound.InnerException?.Message,
                };
                break;
            case ValidationException validation:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = "Validation error occurred.",
                    Status = (int)statusCode,
                    Type = nameof(validation),
                    // Detail = validation.Message,
                };
                var messages = validation.Message.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                problem.Errors = messages;
                break;
            case UnauthorizedAccessException unauthorized:
                statusCode = HttpStatusCode.Unauthorized;
                problem = new CustomProblemDetails
                {
                    Title = unauthorized.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.Unauthorized),
                    Detail = unauthorized.InnerException?.Message,
                };
                break;
            default:
                problem = new CustomProblemDetails
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = ex.StackTrace,
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}
