using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solar.Domain.Entities;
using Solar.Domain.Types;

namespace Solar.Infrastructure.EntitiesConfiguration;

public class ProjetoConfiguration : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        // Configuração da chave primária
        builder.HasKey(p => p.Id);

        // Configuração de Id (Guid)
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        // Configuração do Value Object Nome
        builder.Property(p => p.Nome)
            .HasConversion(
                v => v.Valor,
                v => new Nome(v))
            .HasColumnName("Nome")
            .IsRequired()
            .HasMaxLength(100);

        // Configuração do campo Localizacao
        builder.Property(p => p.Localizacao)
            .HasColumnName("Localizacao")
            .IsRequired()
            .HasMaxLength(100);

        // Configuração do campo DataInicio
        builder.Property(p => p.DataInicio)
            .HasColumnName("DataInicio")
            .IsRequired();

        // Configuração do campo DataFinal (nullable)
        builder.Property(p => p.DataFinal)
            .HasColumnName("DataFinal")
            .IsRequired(false);

        // Configuração do campo ValorTotal
        builder.Property(p => p.ValorTotal)
            .HasColumnName("ValorTotal")
            .IsRequired()
            .HasPrecision(18, 2);

        // Configuração do campo ClienteId (FK)
        builder.Property(p => p.ClienteId)
            .IsRequired();

        // Configuração do campo LiderTecnicoId (FK)
        builder.Property(p => p.LiderTecnicoId)
            .IsRequired();

        // Configuração do relacionamento com Cliente (Muitos Projetos para Um Cliente)
        builder.HasOne(p => p.Cliente)
            .WithMany(c => c.Projetos)
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração do relacionamento com LiderTecnico (Muitos Projetos para Um Técnico Líder)
        builder.HasOne(p => p.LiderTecnico)
            .WithMany(t => t.ProjetosLider)
            .HasForeignKey(p => p.LiderTecnicoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração do relacionamento com EquipeTecnica (Muitos para Muitos)
        builder.HasMany(p => p.EquipeTecnica)
            .WithMany(t => t.ProjetosMembro)
            .UsingEntity("TecnicoEquipeProjeto",
                j => j
                    .HasOne(typeof(Tecnico))
                    .WithMany()
                    .HasForeignKey("TecnicoId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne(typeof(Projeto))
                    .WithMany()
                    .HasForeignKey("ProjetoId")
                    .OnDelete(DeleteBehavior.Cascade));

        // Configuração do relacionamento com Procedimentos (Um Projeto para Muitos Procedimentos)
        builder.HasMany(p => p.Procedimentos)
            .WithOne(proc => proc.Projeto)
            .HasForeignKey(proc => proc.ProjetoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração da tabela
        builder.ToTable("Projetos");
    }
}
