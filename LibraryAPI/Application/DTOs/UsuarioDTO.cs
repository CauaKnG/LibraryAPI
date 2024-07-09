using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Application.DTOs;

public class UsuarioDTO
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Telefone { get; set; }

    public string Endereco { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos.")]
    public string CPF { get; set; }

    public UsuarioDTO(string nome, string telefone, string endereco, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        Endereco = endereco;
        CPF = cpf;
    }
}
