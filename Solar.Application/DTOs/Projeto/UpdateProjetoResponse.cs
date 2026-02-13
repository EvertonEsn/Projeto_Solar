using System.ComponentModel.DataAnnotations;

namespace Solar.Application.DTOs.Projeto;

/// <summary>
/// DTO para retorno de Projeto atualizado
/// </summary>
public class UpdateProjetoResponse
{
    [Required(ErrorMessage = "Id é obrigatório")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "Localização é obrigatória")]
    public required string Localizacao { get; set; }

    [Required(ErrorMessage = "Data de Início é obrigatória")]
    public DateTime DataInicio { get; set; }

    public DateTime? DataFinal { get; set; }

    [Required(ErrorMessage = "Valor Total é obrigatório")]
    public decimal ValorTotal { get; set; }

    [Required(ErrorMessage = "Líder Técnico Id é obrigatório")]
    public Guid LiderTecnicoId { get; set; }
}
