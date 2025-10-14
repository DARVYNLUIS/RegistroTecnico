using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;

namespace RegistroTecnico.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }
        public DbSet<Sistemas> Sistemas { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }  
        public DbSet<PrestamosDetalle> PrestamosDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.Tecnicos)
                .WithMany()
                .HasForeignKey(t => t.TecnicoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PrestamosDetalle>()
                .HasOne(pd => pd.Prestamo)
                .WithMany(p => p.PrestamosDetalles)
                .HasForeignKey(pd => pd.PrestamoId);
        }
    }
}