using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Repositories.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> GetAllAsync();
        Task<Livro> GetByIdAsync(int id);
        Task AddAsync(Livro livro);
        Task UpdateAsync(Livro livro);
        Task DeleteAsync(int id);
    }
}
