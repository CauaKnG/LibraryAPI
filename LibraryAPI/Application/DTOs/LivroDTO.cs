namespace LibraryAPI.Application.DTOs;

public class LivroDTO
{


    public string Titulo { get; set; }

    public string Autor { get; set; }

    public string Editora { get; set; }

    public string Genero { get; set; }

    public bool Disponivel { get; set; }

    public LivroDTO(string titulo, string autor, string editora, string genero, bool disponivel = true)
    {
        Titulo = titulo;
        Autor = autor;
        Editora = editora;
        Genero = genero;
        Disponivel = disponivel;
    }
}
