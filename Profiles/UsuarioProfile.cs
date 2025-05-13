using AutoMapper;
using TesteSeniors.Data.Dtos;
using TesteSeniors.Models;

namespace TesteSeniors.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<CriaUsuarioDto, Usuario>();
            CreateMap<Usuario, BuscaUsuarioDto>();
        }
    }
}
