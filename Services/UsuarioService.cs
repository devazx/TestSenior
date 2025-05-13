using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TesteSeniors.Data.Dtos;
using TesteSeniors.Models;

namespace TesteSeniors.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        private UsuarioService(TokenService tokenService, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IMapper mapper)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CadastraUsuario(CriaUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);

        }


    }
}
