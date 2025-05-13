using Microsoft.AspNetCore.Identity;

namespace TesteSeniors.Models
{
    public class Usuario : IdentityUser
    {
        // Utilizado o identity User, por ja possuir como base algumas validacoes e regras de negocio
        public string Nome { get; set; }
        public int CPF { get; set; }
        public string UF { get; set; }
        public DateTime DataNascimento { get; set; }
        public Usuario(): base() { }
    }
}
