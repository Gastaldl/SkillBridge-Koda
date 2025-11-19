using Microsoft.AspNetCore.Mvc;
using SkillBridge.Domain;
using SkillBridge.Domain.Interfaces;
using Asp.Versioning;

namespace SkillBridge.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")] // <--- Adicione isso
    [Route("api/v{version:apiVersion}/[controller]")] // <--- Mude a rota para isso
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            var usuarios = await _service.ListarUsuarios();
            return Ok(usuarios);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(long id)
        {
            var usuario = await _service.BuscarPorId(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            try
            {
                var novoUsuario = await _service.CadastrarUsuario(usuario);
                return CreatedAtAction(nameof(Get), new { id = novoUsuario.Id }, novoUsuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Usuario usuario)
        {
            try
            {
                await _service.AtualizarUsuario(id, usuario);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.ExcluirUsuario(id);
            return NoContent();
        }
    }
}