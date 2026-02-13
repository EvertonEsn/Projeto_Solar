using Solar.Domain.Types;
using Solar.Domain.Validation;

namespace Solar.Domain.Entities;

public class Projeto : Entity
{
    public Nome Nome { get; set; }

    public string Localizacao { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime? DataFinal { get; set; }

    public decimal ValorTotal { get; set; }

    public Guid ClienteId { get; set; }

    public Cliente Cliente { get; set; }

    public Guid LiderTecnicoId { get; set; }

    public Tecnico LiderTecnico { get; set; }

    public ICollection<Tecnico> EquipeTecnica { get; set; }

    public ICollection<Procedimento> Procedimentos { get; set; }

    public Projeto() { }

    public Projeto(string nome, string localizacao, DateTime dataInicio, DateTime dataFinal,
        decimal valorTotal, Guid clienteId, Guid liderTecnicoId)
    {
        Nome = new Nome(nome);
        DataInicio = DateTime.Now;
        DataFinal = null;
        ValidateDomain(localizacao, ValorTotal);

        Localizacao = localizacao;
        ValorTotal = valorTotal;
        ClienteId = clienteId;
        LiderTecnicoId = liderTecnicoId;
    }

    private void ValidateDomain(string localizacao,
        decimal valorTotal)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(localizacao),
            "Localizacao é obrigatório.");

        DomainExceptionValidation.When(localizacao.Length < 3,
            "A localizacao pode ter no mínimo 3 caracteres.");

        DomainExceptionValidation.When(localizacao.Length > 100,
            "A localizacao não pode exceder 100 caracteres.");

        DomainExceptionValidation.When(valorTotal < 0,
            "Valor total não pode ser negativo.");
    }

    private void AlterarDataConclusao(DateTime novaData)
    {
        DomainExceptionValidation.When(novaData < DataInicio,
            "A previsão deve ser futura.");
        
        DataFinal = novaData;
    }

}