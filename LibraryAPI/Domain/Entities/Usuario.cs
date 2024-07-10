namespace LibraryAPI.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }

        public Usuario() { }

        public Usuario(int id, string nome, string telefone, string endereco, string cpf, string username, string senha)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            Endereco = endereco;
            CPF = cpf;
            Username = username;
            Senha = senha;
        }
    }
}
