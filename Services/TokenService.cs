using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteSeniors.Models;

namespace TesteSeniors.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("nome", usuario.Nome),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
                new Claim("loginTimestamp", DateTime.UtcNow.ToString())
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Aquiseriaachavesecretadaempresa"));

            var entrandoCredenciais = new SigningCredentials
                (
                    chave, SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken
                (
                    expires: DateTime.UtcNow.AddMinutes(20),
                    claims: claims,
                    signingCredentials: entrandoCredenciais
                );

            return new JwtSecurityTokenHandler().WriteToken( token );
        }
    }
}