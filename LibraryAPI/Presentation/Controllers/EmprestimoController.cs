using LibraryAPI.Domain.Interfaces;
using LibraryAPI.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace LibraryAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;

        public EmprestimoController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EmprestimoDTO>>> GetAll()
        {
            try
            {
                var emprestimos = await _emprestimoService.GetAllAsync();
                return Ok(emprestimos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os empréstimos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<EmprestimoDTO>> GetById(int id)
        {
            try
            {
                var emprestimo = await _emprestimoService.GetByIdAsync(id);
                if (emprestimo == null)
                {
                    return NotFound($"Emprestimo com id {id} não encontrado.");
                }
                return Ok(emprestimo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter o empréstimo: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] EmprestimoDTO emprestimo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _emprestimoService.AddAsync(emprestimo);
                return CreatedAtAction(nameof(GetById), new { id = emprestimo.Id }, emprestimo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar o empréstimo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Update(int id, [FromBody] EmprestimoDTO emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return BadRequest("Id do empréstimo no corpo da requisição difere do Id informado na URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _emprestimoService.UpdateAsync(id, emprestimo); 
                return Ok(emprestimo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o empréstimo: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _emprestimoService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir o empréstimo: {ex.Message}");
            }
        }
    }
}
