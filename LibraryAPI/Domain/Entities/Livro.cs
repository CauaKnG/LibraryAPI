namespace LibraryAPI.Domain.Entities
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Genero { get; set; }
        public bool Disponivel { get; set; }

        public Livro()
        {
        }

        public Livro(int id, string titulo, string autor, string editora, string genero, bool disponivel)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            Editora = editora;
            Genero = genero;
            Disponivel = disponivel;
        }
    }
}
