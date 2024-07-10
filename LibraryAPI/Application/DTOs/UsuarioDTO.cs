using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Username { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos.")]
        public string CPF { get; set; }

        public string Senha { get; set; }

        public UsuarioDTO() { }

        public UsuarioDTO(string nome, string telefone, string endereco, string cpf, string username , string senha)
        {
            Nome = nome;
            Telefone = telefone;
            Endereco = endereco;
            CPF = cpf;
            Username = username;
            Senha = senha;
        }
    }

    public class UserAuthorization
    {
        public string Username { get; set; }
        public string Senha { get; set; }
    }
}
