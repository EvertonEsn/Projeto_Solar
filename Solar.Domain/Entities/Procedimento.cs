using Solar.Domain.Validation;

namespace Solar.Domain.Entities;

public class Procedimento : Entity
{
    public string Descricao { get; set; }
    
    public bool Concluido { get; set; }
    
    public DateTime? DataConclusao { get; set; }
    
    public Guid ProjetoId { get; set; }
    
    public Projeto Projeto { get; set; }

    public Procedimento() { }

    public Procedimento(string descricao, Guid projetoId)
    {
        Concluido = false;
        DataConclusao = null;
        
        Descricao = descricao;
        ProjetoId = projetoId;
    }

    public void Update(string descricao, bool concluido)
    {
        ValidateDomain(descricao);
        Descricao = descricao;
        AlterarConclusao(concluido);
    }
    
    private void ValidateDomain(string descricao)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(descricao),
            "Descricao é obrigatório.");

        DomainExceptionValidation.When(Descricao.Length < 3,
            "A localizacao pode ter no mínimo 3 caracteres.");

        DomainExceptionValidation.When(Descricao.Length > 300,
            "A localizacao não pode exceder 300 caracteres.");
    }

    private void AlterarConclusao(bool concluido)
    {
        if (Concluido != concluido)
        {
            if (concluido)
            {
                Concluido = true;
                DataConclusao = DateTime.Now;
            }
            else
            {
                Concluido = false;
                DataConclusao = null;
            }
        }
        
    }
}