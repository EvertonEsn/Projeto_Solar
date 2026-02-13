using System.ComponentModel.DataAnnotations;

namespace Solar.Application.DTOs.Procedimento;

/// <summary>
/// DTO para criação de Procedimento
/// </summary>
public class CreateProcedimentoRequest
{
    [Required(ErrorMessage = "Descrição é obrigatória")]
    [StringLength(300, MinimumLength = 3, ErrorMessage = "Descrição deve ter entre 3 e 300 caracteres")]
    public required string Descricao { get; set; }

    [Required(ErrorMessage = "Projeto Id é obrigatório")]
    public Guid ProjetoId { get; set; }
}
