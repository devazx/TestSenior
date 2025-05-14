using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TesteSeniors.Data.Dtos;
using TesteSeniors.Models;
using TesteSeniors.Services;

namespace TesteSeniors.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        public UsuarioService _usuarioService;

        public UserController(UsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }
        [HttpPost("Cadastro")]
        public async Task<IActionResult> CadastraUsuario(CriaUsuarioDto dto)
        {
            await _usuarioService.CadastraUsuario(dto);
            return Ok("Salvo!");
        }
        [HttpGet("BuscaUsuarios")]
        public async Task<ActionResult<BuscaUsuarioDto>> BuscaUsuario()
        {
            var usuario = _usuarioService.BuscaUsuario();

            return Ok(usuario.Result);
        }

        [HttpGet("id/{UF}")]
        public async Task<ActionResult<BuscaUsuarioDto>> RecuperaUsuariosUf(string Uf)
        {
            var usuarios = _usuarioService.RecuperaUsuariosporUF(Uf);

            return Ok(usuarios.Result);

        }

        [HttpGet("uf/{Id}")]
        public async Task<ActionResult<BuscaUsuarioDto>> RecuperaporId(Guid Id)
        {
            var usuario = _usuarioService.recuperaUsuarioporID(Id);

            return Ok(usuario.Result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginUsuarioDto dto)
        {
            var token = await _usuarioService.Login(dto);
            return Ok(token);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaUsuario(Guid Id)
        {
            var userdelete = _usuarioService.DeletaUsuario(Id);

            if(!userdelete.IsCompletedSuccessfully) return NoContent();

            return Ok(userdelete);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaUsuario(Guid Id, [FromBody] BuscaUsuarioDto dto)
        {
            var user = _usuarioService.AtualizaUsuario(Id, dto);

            if (!user.IsCompletedSuccessfully) return NotFound();

            return NoContent();
        }
    }
}
