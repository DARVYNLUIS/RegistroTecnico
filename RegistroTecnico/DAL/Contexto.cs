using RegistroTecnico.Models;
using Microsoft.EntityFrameworkCore;



namespace RegistroTecnico.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Tecnicos> Tecnicos { get; set; }

    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Ciudad> Ciudad { get; set; }
    public DbSet<Tickets> Tickets { get; set; }
}