using LibraryAPI.Application.DTOs;
using LibraryAPI.Domain.Interfaces;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories.Interfaces;
using RabbitMQ.Client;
using System.Text.Json;
using static Dapper.SqlMapper;

namespace LibraryAPI.Application.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public EmprestimoService(IEmprestimoRepository emprestimoRepository , RabbitMQPublisher rabbitMQPublisher)
        {
            _emprestimoRepository = emprestimoRepository;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<IEnumerable<EmprestimoDTO>> GetAllAsync()
        {
            var emprestimos = await _emprestimoRepository.GetAllAsync();
            var emprestimoDTOs = new List<EmprestimoDTO>();

            foreach (var emprestimo in emprestimos)
            {
                emprestimoDTOs.Add(new EmprestimoDTO
                {
                    Id = emprestimo.Id,
                    LivroId = emprestimo.LivroId,
                    UsuarioId = emprestimo.UsuarioId,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao
                });
            }

            return emprestimoDTOs;
        }

        public async Task<EmprestimoDTO?> GetByIdAsync(int id)
        {
            var emprestimo = await _emprestimoRepository.GetByIdAsync(id);

            if (emprestimo == null)
            {
                return null;
            }

            return new EmprestimoDTO
            {
                Id = emprestimo.Id,
                LivroId = emprestimo.LivroId,
                UsuarioId = emprestimo.UsuarioId,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucao
            };
        }

        public async Task<int> AddAsync(EmprestimoDTO emprestimoDto)
        {
            var emprestimo = new Emprestimo
            {
                LivroId = emprestimoDto.LivroId,
                UsuarioId = emprestimoDto.UsuarioId,
                DataEmprestimo = emprestimoDto.DataEmprestimo,
                DataDevolucao = emprestimoDto.DataDevolucao
            };

            await _emprestimoRepository.AddAsync(emprestimo);

            var message = JsonSerializer.Serialize(emprestimoDto);
            _rabbitMQPublisher.Publish(emprestimo, "EmprestimoQueue");

            return emprestimo.Id;
        }

        public async Task UpdateAsync(int id, EmprestimoDTO emprestimoDto)
        {
            var emprestimo = await _emprestimoRepository.GetByIdAsync(id);

            if (emprestimo == null)
            {
                throw new ApplicationException("Emprestimo não encontrado.");
            }

            emprestimo.LivroId = emprestimoDto.LivroId;
            emprestimo.UsuarioId = emprestimoDto.UsuarioId;
            emprestimo.DataEmprestimo = emprestimoDto.DataEmprestimo;
            emprestimo.DataDevolucao = emprestimoDto.DataDevolucao;

            await _emprestimoRepository.UpdateAsync(emprestimo);
        }

        public async Task DeleteAsync(int id)
        {
            var emprestimo = await _emprestimoRepository.GetByIdAsync(id);

            if (emprestimo == null)
            {
                throw new ApplicationException("Emprestimo não encontrado.");
            }

            await _emprestimoRepository.DeleteAsync(id);
        }
    }
}
