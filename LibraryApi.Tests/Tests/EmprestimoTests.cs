using Xunit;
using Moq;
using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories.Interfaces;
using LibraryAPI.Application.DTOs;
using System;
using System.Threading.Tasks;

public class EmprestimoTests
{
    private readonly Mock<IEmprestimoRepository> _mockEmprestimoRepository;
    private readonly EmprestimoService _emprestimoService;

    public EmprestimoTests()
    {
        _mockEmprestimoRepository = new Mock<IEmprestimoRepository>();
        _emprestimoService = new EmprestimoService(_mockEmprestimoRepository.Object);
    }

    [Fact]
    public async Task RealizarEmprestimo_DeveRealizarEmprestimo()
    {
        var novoEmprestimoDTO = new EmprestimoDTO
        {
            LivroId = 1,
            UsuarioId = 1,
            DataEmprestimo = DateTime.UtcNow,
            DataDevolucao = DateTime.UtcNow.AddDays(14)
        };

        await _emprestimoService.AddAsync(novoEmprestimoDTO);

        _mockEmprestimoRepository.Verify(r => r.AddAsync(It.IsAny<Emprestimo>()), Times.Once);
    }

    [Fact]
    public async Task DevolverLivro_DeveDevolverLivro()
    {
        int emprestimoId = 1; 

        var emprestimo = new Emprestimo
        {
            Id = emprestimoId,
            LivroId = 1, 
            UsuarioId = 1, 
            DataEmprestimo = DateTime.UtcNow,
            DataDevolucao = DateTime.UtcNow.AddDays(14)
        };

        _mockEmprestimoRepository.Setup(r => r.GetByIdAsync(emprestimoId)).ReturnsAsync(emprestimo);

        await _emprestimoService.DeleteAsync(emprestimoId);

        _mockEmprestimoRepository.Verify(r => r.DeleteAsync(emprestimoId), Times.Once);
    }

}
