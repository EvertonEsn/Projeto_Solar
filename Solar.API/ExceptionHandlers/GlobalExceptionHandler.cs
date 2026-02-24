using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Solar.API.Exceptions;
using Solar.Domain.Validation;

namespace Solar.API.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"DEBUG: Recebendo exceção do tipo: {exception.GetType().FullName}");

        var (statusCode, title) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Recurso Nao Encontrado"),
            DomainExceptionValidation => (StatusCodes.Status422UnprocessableEntity, "Violação de Regra de Negócio:"),
            AppValidationException => (StatusCodes.Status400BadRequest, "Erro Validação"),
            _ => (StatusCodes.Status500InternalServerError, "Erro Inesperado")
        };
        
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };
        
        if (exception is AppValidationException valEx)
        {
            problemDetails.Extensions.Add("errors", valEx.Errors);
        }
        
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}