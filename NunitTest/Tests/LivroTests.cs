using Xunit;
using Moq;
using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories.Interfaces;


public class LivroTests
{
    private readonly Mock<ILivroRepository> _mockLivroRepository;
    private readonly LivroService _livroService;

    public LivroTests()
    {
        _mockLivroRepository = new Mock<ILivroRepository>();
        _livroService = new LivroService(_mockLivroRepository.Object);
    }

    [Fact]
    public async Task AdicionarNovoLivro()
    {
        var novoLivro = new Livro
        {
            Titulo = "Teste Titulo",
            Autor = "Teste Autor",
            Editora = "Teste Editora",
            Genero = "Teste Genero",
            Disponivel = true
        };

        await _livroService.AddAsync(novoLivro);

        _mockLivroRepository.Verify(r => r.AddAsync(novoLivro), Times.Once);
    }

    [Fact]
    public async Task ObterTodosLivros()
    {
        var livros = new List<Livro>
        {
            new Livro { Id = 1, Titulo = "Livro 1", Autor = "Autor 1", Editora = "Editora 1", Genero = "Genero 1", Disponivel = true },
            new Livro { Id = 2, Titulo = "Livro 2", Autor = "Autor 2", Editora = "Editora 2", Genero = "Genero 2", Disponivel = false }
        };

        _mockLivroRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(livros);

        var resultado = await _livroService.GetAllAsync();

        Xunit.Assert.Equal(2, resultado.Count());
        Xunit.Assert.Contains(resultado, l => l.Titulo == "Livro 1");
        Xunit.Assert.Contains(resultado, l => l.Titulo == "Livro 2");
    }
}
