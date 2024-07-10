using Xunit;
using Moq;
using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    public async Task AdicionarNovoLivro_DeveAdicionarLivro()
    {
        // Arrange
        var novoLivro = new Livro
        {
            Titulo = "Teste Titulo",
            Autor = "Teste Autor",
            Editora = "Teste Editora",
            Genero = "Teste Genero",
            Disponivel = true
        };

        // Act
        await _livroService.AddAsync(novoLivro);

        // Assert
        _mockLivroRepository.Verify(r => r.AddAsync(novoLivro), Times.Once);
    }

    // Outros testes continuariam aqui...
}
