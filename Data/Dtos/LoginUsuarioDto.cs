using System.ComponentModel.DataAnnotations;

namespace TesteSeniors.Data.Dtos
{
    public class LoginUsuarioDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}