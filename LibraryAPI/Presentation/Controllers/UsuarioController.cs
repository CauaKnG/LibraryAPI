using LibraryAPI.Application.DTOs;
using LibraryAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os usuários: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound($"Usuario com id {id} não encontrado.");
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter o usuário: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] UsuarioDTO usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _usuarioService.AddAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar o usuário: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Update(int id, [FromBody] UsuarioDTO usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("Id do usuário no corpo da requisição difere do Id informado na URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _usuarioService.UpdateAsync(id, usuario);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o usuário: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _usuarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir o usuário: {ex.Message}");
            }
        }
    }
}
