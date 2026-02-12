using System.Text.RegularExpressions;
using Solar.Domain.Validation;

namespace Solar.Domain.Types;

public record Email 
{
    public string Endereco { get; }

    public Email(string endereco)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(endereco), "E-mail obrigatório.");
        DomainExceptionValidation.When(!Regex.IsMatch(endereco, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"), "Formato de e-mail inválido.");
        
        Endereco = endereco;
    }
}