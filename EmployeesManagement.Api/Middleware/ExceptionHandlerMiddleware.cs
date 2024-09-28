using System.Net;
using EmployeesManagement.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.Api.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            if (ex is OperationCanceledException)
            {
                context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
                return;
            }

            if (ex is not DomainException domainException)
            {
                _logger.LogError(ex, "Unexpected error happened while processing request");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "Unexpected error"
                });
                return;
            }

            var statusCode = domainException switch
            {
                EntityNotFoundException => HttpStatusCode.NotFound,
                ValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.BadRequest
            };
            context.Response.StatusCode = (int)statusCode;

            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = domainException.Code,
                Detail = domainException.Message,
                Extensions =
                {
                    ["parameters"] = domainException.Parameters
                }
            };
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}