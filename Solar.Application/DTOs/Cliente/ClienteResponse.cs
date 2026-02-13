using System.ComponentModel.DataAnnotations;

namespace Solar.Application.DTOs.Cliente;

/// <summary>
/// DTO para retorno de leitura de Cliente
/// </summary>
public class ClienteResponse
{
    [Required(ErrorMessage = "Id é obrigatório")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    public required string Email { get; set; }
}
