using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<Usuario?> GetByUsernameAndPasswordAsync(string username, string senha);
    }
}
