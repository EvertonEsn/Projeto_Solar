using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solar.Domain.Entities;
using Solar.Domain.Types;

namespace Solar.Infrastructure.EntitiesConfiguration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        // Configuração da chave primária
        builder.HasKey(c => c.Id);

        // Configuração de Id (auto-incremento)
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        // Configuração do Value Object Nome
        builder.Property(c => c.Nome)
            .HasConversion(
                v => v.Valor,
                v => new Nome(v))
            .HasColumnName("Nome")
            .IsRequired()
            .HasMaxLength(100);

        // Configuração do Value Object Email
        builder.Property(c => c.Email)
            .HasConversion(
                v => v.Endereco,
                v => new Email(v))
            .HasColumnName("Email")
            .IsRequired()
            .HasMaxLength(255);

        // Configuração do relacionamento com Projeto (Um Cliente para Muitos Projetos)
        builder.HasMany(c => c.Projetos)
            .WithOne(p => p.Cliente)
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração da tabela
        builder.ToTable("Clientes");
    }
}


