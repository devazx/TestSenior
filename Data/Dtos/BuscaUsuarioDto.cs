namespace TesteSeniors.Data.Dtos
{
    public class BuscaUsuarioDto
    {
        public Guid Id { get; set; }    
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string UF { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
