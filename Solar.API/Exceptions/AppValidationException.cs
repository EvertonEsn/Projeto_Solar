using FluentValidation.Results;

namespace Solar.API.Exceptions;

public class AppValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public AppValidationException(IEnumerable<ValidationFailure> failures) 
        : base("Um ou mais erros de validação ocorreram.")
    {
        Errors = failures
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key, 
                g => g.Select(x => x.ErrorMessage).ToArray()
            );
    }
}