using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.SharedKernel;
using StaffManagement.Infrastructure.SharedKernel;
using ILogger = Serilog.ILogger;

namespace StaffManagement.Infrastructure;

public class ProblemDetailsExceptionFilter : IExceptionFilter
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly ILogger _logger;

    public ProblemDetailsExceptionFilter(ProblemDetailsFactory problemDetailsFactory,
        ILogger logger)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        ProblemDetails problemDetails;
        switch (context.Exception)
        {
            case DomainException exception:
                problemDetails = _problemDetailsFactory.CreateProblemDetails(context.HttpContext, 400,
                    title: "Domain exception",
                    type: "domain_error",
                    detail: exception.Message);
                break;
            case DbConcurrencyException exception:
                problemDetails = _problemDetailsFactory.CreateProblemDetails(context.HttpContext, 409,
                    title: "Concurrency error",
                    type: "concurrency_error",
                    detail: exception.Message);
                break;
            case DatabaseException exception:
                problemDetails = _problemDetailsFactory.CreateProblemDetails(context.HttpContext, 400,
                    title: "Database error",
                    type: "database_error",
                    detail: exception.Message);
                break;
            case NotFoundException exception:
                problemDetails = _problemDetailsFactory.CreateProblemDetails(context.HttpContext, 404,
                    title: "Not found exception",
                    type: "not_found",
                    detail: exception.Message);
                break;
            default:
                _logger.Error(context.Exception, "Unhandled exception");
                problemDetails = _problemDetailsFactory.CreateProblemDetails(context.HttpContext, 500,
                    "Internal error",
                    detail: "Internal error occurred.");
                break;
        }

        context.Result =
            new ObjectResult(problemDetails);
        context.ExceptionHandled = true;
    }
}