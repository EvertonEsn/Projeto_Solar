using System.ComponentModel.DataAnnotations;
using Solar.Application.Validation.ProjetoValidators;

namespace Solar.Application.DTOs.Projeto;

/// <summary>
/// DTO para requisição de atualização de Projeto
/// </summary>
public class UpdateProjetoRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 200 caracteres")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "Localização é obrigatória")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Localização deve ter entre 3 e 100 caracteres")]
    public required string Localizacao { get; set; }

    [Required(ErrorMessage = "Data de Início é obrigatória")]
    public DateTime DataInicio { get; set; }

    public DateTime? DataFinal { get; set; }

    [Required(ErrorMessage = "Valor Total é obrigatório")]
    [Range(0, double.MaxValue, ErrorMessage = "Valor Total não pode ser negativo")]
    public decimal ValorTotal { get; set; }

    [Required(ErrorMessage = "Líder Técnico Id é obrigatório")]
    public Guid LiderTecnicoId { get; set; }
}
