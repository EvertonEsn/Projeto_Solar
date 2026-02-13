using System.ComponentModel.DataAnnotations;

namespace Solar.Application.DTOs.Procedimento;

/// <summary>
/// DTO para requisição de atualização de Procedimento
/// </summary>
public class UpdateProcedimentoRequest
{
    [Required(ErrorMessage = "Descrição é obrigatória")]
    [StringLength(300, MinimumLength = 3, ErrorMessage = "Descrição deve ter entre 3 e 300 caracteres")]
    public required string Descricao { get; set; }

    [Required(ErrorMessage = "Concluído é obrigatório")]
    public bool Concluido { get; set; }
}
