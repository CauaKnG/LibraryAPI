using LibraryAPI.Application.DTOs;


namespace LibraryAPI.Domain.Interfaces
{
    public interface IEmprestimoService
    {
        Task<IEnumerable<EmprestimoDTO>> GetAllAsync();
        Task<EmprestimoDTO?> GetByIdAsync(int id);
        Task<int> AddAsync(EmprestimoDTO emprestimoDto);
        Task UpdateAsync(int id, EmprestimoDTO emprestimoDto);
        Task DeleteAsync(int id);
    }
}
