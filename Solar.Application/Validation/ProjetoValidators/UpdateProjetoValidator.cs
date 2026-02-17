using FluentValidation;
using Solar.Application.DTOs.Projeto;

namespace Solar.Application.Validation.ProjetoValidators;

public class UpdateProjetoValidator : AbstractValidator<UpdateProjetoRequest>
{
    public UpdateProjetoValidator()
    {
        RuleFor(x => x.DataFinal)
            .GreaterThan(x => x.DataInicio)
            .WithMessage("O término deve ser após o início.");
        
        RuleFor(x => x.DataInicio)
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("A data de início não pode ser anterior a hoje.");
    }
}