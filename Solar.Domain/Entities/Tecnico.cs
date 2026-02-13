using Solar.Domain.Types;
using Solar.Domain.Validation;

namespace Solar.Domain.Entities;

public class Tecnico : Entity
{
    public Nome Nome { get; private set; }
    
    public string Cargo { get; private set; }
    
    public ICollection<Projeto> ProjetosLider { get; set; }
    public ICollection<Projeto> ProjetosMembro { get; set; }

    public Tecnico() { }

    public Tecnico(string nome, string cargo)
    {
        Nome = new Nome(nome);
        ValidateDomain(cargo);

        Cargo = cargo;
    }
    
    public void Update(string novoNome, string novoCargo)
    {
        Nome = new Nome(novoNome);
        ValidateDomain(novoCargo);

        Cargo = novoCargo;
    }

    private void ValidateDomain(string cargo)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(cargo), 
            "O cargo é obrigatório.");
            
        DomainExceptionValidation.When(cargo.Length < 3, 
            "O cargo deve ter no mínimo 3 caracteres.");
        
        DomainExceptionValidation.When(cargo.Length > 50, 
            "O cargo não pode exceder 50 caracteres.");
    }
}