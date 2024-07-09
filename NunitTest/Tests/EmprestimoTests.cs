using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories.Interfaces;
using Moq;
using Xunit;

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
    public async Task RealizarEmprestimo()
    {
        var novoEmprestimo = new EmprestimoDTO
        {
            LivroId = 1,
            UsuarioId = 1,
            DataEmprestimo = DateTime.Now,
            DataDevolucao = null
        };

        await _emprestimoService.AddAsync(novoEmprestimo);

        _mockEmprestimoRepository.Verify(r => r.AddAsync(It.Is<Emprestimo>(e => e.LivroId == novoEmprestimo.LivroId && e.UsuarioId == novoEmprestimo.UsuarioId)), Times.Once);
    }
    [Fact]
    public async Task DevolverLivro()
    {
        var emprestimo = new Emprestimo
        {
            Id = 1,
            LivroId = 1,
            UsuarioId = 1,
            DataEmprestimo = DateTime.Now.AddDays(-7),
            DataDevolucao = null
        };

        _mockEmprestimoRepository.Setup(r => r.GetByIdAsync(emprestimo.Id)).ReturnsAsync(emprestimo);

        var emprestimoDto = new EmprestimoDTO
        {
            Id = 1,
            LivroId = 1,
            UsuarioId = 1,
            DataEmprestimo = emprestimo.DataEmprestimo,
            DataDevolucao = DateTime.Now
        };

        await _emprestimoService.UpdateAsync(emprestimoDto.Id, emprestimoDto);

        _mockEmprestimoRepository.Verify(r => r.UpdateAsync(It.Is<Emprestimo>(e => e.DataDevolucao == emprestimoDto.DataDevolucao)), Times.Once);
    }

}
