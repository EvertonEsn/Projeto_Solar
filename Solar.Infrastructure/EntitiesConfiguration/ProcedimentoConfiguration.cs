using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solar.Domain.Entities;

namespace Solar.Infrastructure.EntitiesConfiguration;

public class ProcedimentoConfiguration : IEntityTypeConfiguration<Procedimento>
{
    public void Configure(EntityTypeBuilder<Procedimento> builder)
    {
        // Configuração da chave primária
        builder.HasKey(proc => proc.Id);

        // Configuração de Id (Guid)
        builder.Property(proc => proc.Id)
            .ValueGeneratedOnAdd();

        // Configuração do campo Descricao
        builder.Property(proc => proc.Descricao)
            .HasColumnName("Descricao")
            .IsRequired()
            .HasMaxLength(300);

        // Configuração do campo Concluido
        builder.Property(proc => proc.Concluido)
            .HasColumnName("Concluido")
            .IsRequired()
            .HasDefaultValue(false);

        // Configuração do campo DataConclusao (nullable)
        builder.Property(proc => proc.DataConclusao)
            .HasColumnName("DataConclusao")
            .IsRequired(false);

        // Configuração do campo ProjetoId (FK)
        builder.Property(proc => proc.ProjetoId)
            .IsRequired();

        // Configuração do relacionamento com Projeto (Muitos Procedimentos para Um Projeto)
        builder.HasOne(proc => proc.Projeto)
            .WithMany(p => p.Procedimentos)
            .HasForeignKey(proc => proc.ProjetoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração da tabela
        builder.ToTable("Procedimentos");
    }
}
