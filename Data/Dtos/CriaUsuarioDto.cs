using System.ComponentModel.DataAnnotations;

namespace TesteSeniors.Data.Dtos
{
    public class CriaUsuarioDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public long CPF { get; set; }
        [Required]
        public string UF { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]
        [Compare("Senha")]
        public string ReSenha { get; set; }
    }
}
