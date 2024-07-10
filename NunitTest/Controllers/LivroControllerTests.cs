using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LibraryAPI.Tests.Controllers
{
    public class LivroControllerTests
    {
        [Fact]
        public async Task LivroController_AddNovoLivro_ReturnsCreatedResponse()
        {
            // Arrange
            var mockLivroService = new Mock<LivroService>();
            var livroController = new LivroController(mockLivroService.Object);
            var novoLivro = new Livro { Id = 1, Titulo = "Livro de Teste", Autor = "Autor Teste" };

            // Act
            var result = await livroController.Add(novoLivro) as CreatedAtActionResult;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(nameof(LivroController.GetById), result.ActionName);
            Xunit.Assert.Equal(201, result.StatusCode);
            var livro = Xunit.Assert.IsType<Livro>(result.Value);
            Xunit.Assert.Equal(novoLivro.Id, livro.Id);
        }
    }
}
