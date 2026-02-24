using FluentValidation;
using Solar.Application.DTOs.Projeto;
using Solar.Domain.Interfaces;

namespace Solar.Application.Validation.ProjetoValidators;

public class CreateProjetoValidator : AbstractValidator<CreateProjetoRequest>
{

    private readonly IClienteRepository _clienteRepository;

    private readonly ITecnicoRepository _tecnicoRepository;
    
    public CreateProjetoValidator(IClienteRepository clienteRepository, ITecnicoRepository tecnicoRepository)
    {
        _clienteRepository = clienteRepository;
        _tecnicoRepository = tecnicoRepository;
        
        RuleFor(p => p.ClienteId)
            .MustAsync(async (id, cancellation) =>
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);

                return cliente != null;
            })
            .WithMessage("Cliente informado não existe.");
        
        RuleFor(p => p.LiderTecnicoId)
            .MustAsync(async (id, cancellation) =>
            {
                var tecnico = await _tecnicoRepository.GetByIdAsync(id);

                return tecnico != null;
            })
            .WithMessage("Lider tecnico informado não existe.");
    }
    
}