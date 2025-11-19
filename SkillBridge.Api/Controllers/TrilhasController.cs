using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SkillBridge.Domain;
using SkillBridge.Domain.Exceptions;
using SkillBridge.Domain.Interfaces;

namespace SkillBridge.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TrilhasController : ControllerBase
    {
        private readonly ITrilhaService _service;

        public TrilhasController(ITrilhaService service)
        {
            _service = service;
        }

        // GET: api/v1/Trilhas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trilha>>> Get()
        {
            var trilhas = await _service.ListarTrilhas();
            return Ok(trilhas);
        }

        // GET: api/v1/Trilhas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trilha>> Get(long id)
        {
            try
            {
                var trilha = await _service.BuscarPorId(id);
                return Ok(trilha);
            }
            catch (TrilhaNaoEncontradaException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        // POST: api/v1/Trilhas
        [HttpPost]
        public async Task<ActionResult<Trilha>> Post(Trilha trilha)
        {
            try
            {
                var novaTrilha = await _service.CriarTrilha(trilha);
                return CreatedAtAction(nameof(Get), new { id = novaTrilha.Id, version = "1" }, novaTrilha);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // PUT: api/v1/Trilhas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Trilha trilha)
        {
            try
            {
                await _service.AtualizarTrilha(id, trilha);
                return NoContent();
            }
            catch (TrilhaNaoEncontradaException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // DELETE: api/v1/Trilhas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _service.ExcluirTrilha(id);
                return NoContent();
            }
            catch (TrilhaNaoEncontradaException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}