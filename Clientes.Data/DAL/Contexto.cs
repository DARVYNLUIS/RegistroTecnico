using Microsoft.EntityFrameworkCore;
using Clientes.Data.Models;

namespace Clientes.Data.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
     

   
    }
}