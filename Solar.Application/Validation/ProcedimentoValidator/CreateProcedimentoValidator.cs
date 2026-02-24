using FluentValidation;
using Solar.Application.DTOs.Procedimento;
using Solar.Domain.Interfaces;

namespace Solar.Application.Validation.ProcedimentoValidator;

public class CreateProcedimentoValidator : AbstractValidator<CreateProcedimentoRequest>
{
    private readonly IProjetoRepository _projetoRepository;
    
    public CreateProcedimentoValidator(IProjetoRepository projetoRepository)
    {
        _projetoRepository = projetoRepository;
        
        RuleFor(p => p.ProjetoId)
            .MustAsync(async (id, cancellation) =>
            {
                var cliente = await _projetoRepository.GetByIdAsync(id);

                return cliente != null;
            })
            .WithMessage("Projeto informado não existe.");
    }
}