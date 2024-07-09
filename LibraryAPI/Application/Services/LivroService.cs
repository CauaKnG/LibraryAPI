using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories.Interfaces;

namespace LibraryAPI.Application.Services
{
    public class LivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            return await _livroRepository.GetAllAsync();
        }

        public async Task<Livro> GetByIdAsync(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Livro livro)
        {
            await _livroRepository.AddAsync(livro);
        }

        public async Task UpdateAsync(Livro livro)
        {
            await _livroRepository.UpdateAsync(livro);
        }

        public async Task DeleteAsync(int id)
        {
            await _livroRepository.DeleteAsync(id);
        }
    }
}