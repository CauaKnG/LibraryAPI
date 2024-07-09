namespace LibraryAPI.Domain.Entities
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }

        public Emprestimo()
        {
        }

        public Emprestimo(int id, int livroId, int usuarioId, DateTime dataEmprestimo, DateTime? dataDevolucao)
        {
            Id = id;
            LivroId = livroId;
            UsuarioId = usuarioId;
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;
        }
    }
}
