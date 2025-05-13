using Microsoft.EntityFrameworkCore;
using TesteSeniors.Models;

namespace TesteSeniors.Data
{
    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
