using System.ComponentModel.DataAnnotations;

namespace Solar.Application.DTOs.Tecnico;

/// <summary>
/// DTO para retorno de Tecnico atualizado
/// </summary>
public class UpdateTecnicoResponse
{
    [Required(ErrorMessage = "Id é obrigatório")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "Cargo é obrigatório")]
    public required string Cargo { get; set; }
}
