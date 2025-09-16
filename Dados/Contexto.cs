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
        public DbSet<LogEvento> LogEventos { get; set; }
        public DbSet<AnoSafra> AnoSafras { get; set; }
        public DbSet<Despesa> Despesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DespesaCategoria>().ToTable("DespesaCategorias");
            modelBuilder.Entity<AnoSafra>().ToTable("AnoSafras");

            modelBuilder.Entity<LogEvento>(entity =>
            {
                entity.ToTable("LogEvento");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Message).HasColumnType("nvarchar(max)");
                entity.Property(e => e.MessageTemplate).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Level).HasMaxLength(128);
                entity.Property(e => e.TimeStamp).IsRequired();
                entity.Property(e => e.Exception).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Properties).HasColumnType("nvarchar(max)");
            });

            modelBuilder.Entity<Despesa>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Descricao)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(d => d.Valor)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.AnoSafra)
                      .WithMany()
                      .HasForeignKey(d => d.AnoSafraId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DespesaCategoria)
                      .WithMany()
                      .HasForeignKey(d => d.DespesaCategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

