using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Repositories.Interfaces
{
    public interface IEmprestimoRepository
    {
        Task<IEnumerable<Emprestimo>> GetAllAsync();
        Task<Emprestimo?> GetByIdAsync(int id); 
        Task AddAsync(Emprestimo emprestimo);
        Task UpdateAsync(Emprestimo emprestimo);
        Task DeleteAsync(int id);
    }
}
