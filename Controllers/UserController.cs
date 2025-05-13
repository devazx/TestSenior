using Microsoft.AspNetCore.Mvc;
using TesteSeniors.Data.Dtos;
using TesteSeniors.Services;

namespace TesteSeniors.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
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
            return Ok("Usuario Cadastrado!");
        }
        [HttpGet("BuscaUsuarios")]
        public async Task<ActionResult<BuscaUsuarioDto>> BuscaUsuario()
        {
            var usuario = _usuarioService.BuscaUsuario();

            return Ok(usuario.Result);
        }
    }
}
