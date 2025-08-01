using Agro.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agro.Dados
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public Contexto(DbContextOptions<Contexto> options)
        : base(options) { }
    }
}
