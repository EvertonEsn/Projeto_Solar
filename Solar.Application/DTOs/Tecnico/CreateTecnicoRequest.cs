using System.ComponentModel.DataAnnotations;

namespace Solar.Application.DTOs.Tecnico;

/// <summary>
/// DTO para criação de Tecnico
/// </summary>
public class CreateTecnicoRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 200 caracteres")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "Cargo é obrigatório")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Cargo deve ter entre 3 e 50 caracteres")]
    public required string Cargo { get; set; }
}
