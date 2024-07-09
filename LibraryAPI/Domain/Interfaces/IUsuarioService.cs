using LibraryAPI.Application.DTOs;

namespace LibraryAPI.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO?> GetByIdAsync(int id);
        Task<int> AddAsync(UsuarioDTO usuarioDto);
        Task UpdateAsync(int id, UsuarioDTO usuarioDto);
        Task DeleteAsync(int id);
    }
}
