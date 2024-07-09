using LibraryAPI.Application.DTOs;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces;
using LibraryAPI.Domain.Repositories.Interfaces;

namespace LibraryAPI.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(usuario => new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Telefone = usuario.Telefone,
                Endereco = usuario.Endereco,
                CPF = usuario.CPF
            }).ToList();
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return null;
            }
            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Telefone = usuario.Telefone,
                Endereco = usuario.Endereco,
                CPF = usuario.CPF
            };
        }

        public async Task<int> AddAsync(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Telefone = usuarioDto.Telefone,
                Endereco = usuarioDto.Endereco,
                CPF = usuarioDto.CPF
            };
            await _usuarioRepository.AddAsync(usuario);
            return usuario.Id;
        }

        public async Task UpdateAsync(int id, UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario
            {
                Id = id,
                Nome = usuarioDto.Nome,
                Telefone = usuarioDto.Telefone,
                Endereco = usuarioDto.Endereco,
                CPF = usuarioDto.CPF
            };
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }
    }
}
