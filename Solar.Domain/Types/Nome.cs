using Solar.Domain.Validation;

namespace Solar.Domain.Types;

public record Nome
{
    public string Valor { get; }

    public Nome(string valor)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(valor), 
            "O nome é obrigatório.");
            
        DomainExceptionValidation.When(valor.Length < 3, 
            "O nome deve ter no mínimo 3 caracteres.");
            
        DomainExceptionValidation.When(valor.Length > 100, 
            "O nome não pode exceder 100 caracteres.");

        Valor = valor;
    }

    // Facilita a conversão implícita para string quando necessário
    public static implicit operator string(Nome nome) => nome.Valor;
}