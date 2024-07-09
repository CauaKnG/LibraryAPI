using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace LibraryAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly LivroService _livroService;

        public LivroController(LivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetAll()
        {
            var livro = await _livroService.GetAllAsync();
            return Ok(livro);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetById(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _livroService.AddAsync(livro);
            return CreatedAtAction(nameof(GetById), new { id = livro.Id }, livro);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Livro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest("Id do livro no corpo da requisição difere do Id informado na URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _livroService.UpdateAsync(livro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o livro: {ex.Message}");
            }

            return Ok(livro); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var livroExistente = await _livroService.GetByIdAsync(id);
            if (livroExistente == null)
            {
                return NotFound();
            }

            await _livroService.DeleteAsync(id);
            return NoContent();
        }
    }
}