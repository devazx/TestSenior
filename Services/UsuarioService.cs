using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using TesteSeniors.Data;
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
        private UsuarioDbContext _userContext;

        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService, UsuarioDbContext userContext, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task CadastraUsuario(CriaUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Senha);

            //if (!resultado.Succeeded) throw new ApplicationException("Falha ao cadastrar usuario.");
        }

        public async Task<IEnumerable> BuscaUsuario()
        {
            var LocalizaUser =  _mapper.Map<List<BuscaUsuarioDto>>(_userContext.Usuarios.ToList());

            return LocalizaUser;
        }

        public async Task<string> Login(LoginUsuarioDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(dto.Nome, dto.Senha, false, false);
            if (!resultado.Succeeded) throw new ApplicationException("Usuario nao autenticado");

            var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.Nome.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        } 


    }
}
