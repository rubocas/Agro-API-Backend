using Agro.API.Entidades;
using Agro.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agro.Dados
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public Contexto(DbContextOptions<Contexto> options)
        : base(options) { }

        public DbSet<DespesaCategoria> DespesasCategorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DespesaCategoria>().ToTable("DespesaCategorias");
        }
    }
}
