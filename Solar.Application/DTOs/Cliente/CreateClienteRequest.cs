using System.ComponentModel.DataAnnotations;

namespace Solar.Application.DTOs.Cliente;

/// <summary>
/// DTO para criação de Cliente
/// </summary>
public class CreateClienteRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 200 caracteres")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public required string Email { get; set; }
}
