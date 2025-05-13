using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TesteSeniors.Models;

namespace TesteSeniors.Data
{
    public class UsuarioDbContext : IdentityDbContext<Usuario>
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
