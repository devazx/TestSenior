using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
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
            usuario.UserName = dto.Nome;
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Senha);

            var erros = string.Join(", ", resultado.Errors.Select(e => e.Description));

            if (!resultado.Succeeded) { throw new ApplicationException($"Falha ao cadastrar: {erros}"); }
        }

        public async Task<IEnumerable> BuscaUsuario()
        {
            var LocalizaUser = _mapper.Map<List<BuscaUsuarioDto>>(_userContext.Usuarios.ToList());

            return LocalizaUser;
        }

        public async Task<IEnumerable> RecuperaUsuariosporUF(string Uf)
        {
            var usuarios = await _userContext.Usuarios
                .Where(u => u.UF.Equals(Uf))
                .ToListAsync();

            var LocalizaUser = _mapper.Map<List<BuscaUsuarioDto>>(usuarios);

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
        public async Task<BuscaUsuarioDto> recuperaUsuarioporID(Guid id)
        {
            Usuario user = _userContext.Usuarios.FirstOrDefault(u => u.Id == id.ToString());

            BuscaUsuarioDto userDto = _mapper.Map<BuscaUsuarioDto>(user);

            return userDto;
        }

        public async Task DeletaUsuario(Guid id)
        {
            var usuario = _userContext.Usuarios.FirstOrDefault(u => u.Id == id.ToString());

            _userContext.Remove(usuario);
            _userContext.SaveChanges();
        }

        public async Task AtualizaUsuario(Guid Id, [FromBody] BuscaUsuarioDto dto)
        {
            Usuario user = await _userContext.Usuarios.FirstOrDefaultAsync(u => u.Id == Id.ToString());

            _mapper.Map(dto, user);
            _userContext.SaveChanges();
        }


    }
}
