using System.ComponentModel.DataAnnotations;

namespace Solar.Application.DTOs.Procedimento;

/// <summary>
/// DTO para retorno de leitura de Procedimento
/// </summary>
public class ProcedimentoResponse
{
    [Required(ErrorMessage = "Id é obrigatório")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Descrição é obrigatória")]
    public required string Descricao { get; set; }

    [Required(ErrorMessage = "Concluído é obrigatório")]
    public bool Concluido { get; set; }

    public DateTime? DataConclusao { get; set; }

    [Required(ErrorMessage = "Projeto Id é obrigatório")]
    public Guid ProjetoId { get; set; }
}
