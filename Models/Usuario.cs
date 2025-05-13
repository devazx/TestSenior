namespace TesteSeniors.Models
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public int CPF { get; set; }
        public string UF { get; set; }
        public DateTime DatadeNascimento { get; set; }

    }
}
