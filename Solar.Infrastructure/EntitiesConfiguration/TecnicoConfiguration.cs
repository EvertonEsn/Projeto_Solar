using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solar.Domain.Entities;
using Solar.Domain.Types;

namespace Solar.Infrastructure.EntitiesConfiguration;

public class TecnicoConfiguration : IEntityTypeConfiguration<Tecnico>
{
    public void Configure(EntityTypeBuilder<Tecnico> builder)
    {
        // Configuração da chave primária
        builder.HasKey(t => t.Id);

        // Configuração de Id (Guid)
        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        // Configuração do Value Object Nome
        builder.Property(t => t.Nome)
            .HasConversion(
                v => v.Valor,
                v => new Nome(v))
            .HasColumnName("Nome")
            .IsRequired()
            .HasMaxLength(100);

        // Configuração do campo Cargo
        builder.Property(t => t.Cargo)
            .HasColumnName("Cargo")
            .IsRequired()
            .HasMaxLength(50);

        // Configuração do relacionamento ProjetosLider (Um Técnico para Muitos Projetos como Lider)
        builder.HasMany(t => t.ProjetosLider)
            .WithOne(p => p.LiderTecnico)
            .HasForeignKey(p => p.LiderTecnicoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração do relacionamento ProjetosMembro (Muitos para Muitos - Técnico e Projeto)
        builder.HasMany(t => t.ProjetosMembro)
            .WithMany(p => p.EquipeTecnica)
            .UsingEntity("TecnicoEquipeProjeto",
                j => j
                    .HasOne(typeof(Projeto))
                    .WithMany()
                    .HasForeignKey("ProjetoId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne(typeof(Tecnico))
                    .WithMany()
                    .HasForeignKey("TecnicoId")
                    .OnDelete(DeleteBehavior.Cascade));

        // Configuração da tabela
        builder.ToTable("Tecnicos");
    }
}
