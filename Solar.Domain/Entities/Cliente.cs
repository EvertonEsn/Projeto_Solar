using Solar.Domain.Types;

namespace Solar.Domain.Entities;

public sealed class Cliente : Entity
{
    public Nome Nome { get; private set; }
    
    public Email Email { get; private set; }
    
    public ICollection<Projeto> Projetos { get; set; }

    public Cliente(string nome, string email)
    {
        Nome = new Nome(nome);
        Email = new Email(email);
    }
    
    public void Update(string novoNome, string novoEmail)
    {
        Nome = new Nome(novoNome);
        Email = new Email(novoEmail);
    }
}