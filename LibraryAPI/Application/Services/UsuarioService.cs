using BCrypt.Net;
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
                CPF = usuario.CPF,
                Username = usuario.Username
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
                CPF = usuario.CPF,
                Username = usuario.Username
            };
        }

        public async Task<int> AddAsync(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Telefone = usuarioDto.Telefone,
                Endereco = usuarioDto.Endereco,
                CPF = usuarioDto.CPF,
                Username = usuarioDto.Username,
                Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha) 
            };
            await _usuarioRepository.AddAsync(usuario);
            return usuario.Id;
        }

        public async Task UpdateAsync(int id, UsuarioDTO usuarioDto)
        {
            var existingUser = await _usuarioRepository.GetByIdAsync(id);
            if (existingUser != null)
            {
                existingUser.Nome = usuarioDto.Nome;
                existingUser.Telefone = usuarioDto.Telefone;
                existingUser.Endereco = usuarioDto.Endereco;
                existingUser.CPF = usuarioDto.CPF;
                existingUser.Username = usuarioDto.Username;
                if (!string.IsNullOrEmpty(usuarioDto.Senha))
                {
                    existingUser.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);
                }
                await _usuarioRepository.UpdateAsync(existingUser);
            }
        }


        public async Task DeleteAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }
    }
}
