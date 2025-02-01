using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
    public class TicketService
    {
        private readonly IDbContextFactory<Contexto> DbFactory;

        public TicketService(IDbContextFactory<Contexto> DbFactory)
        {
            this.DbFactory = DbFactory;
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets.AnyAsync(t => t.TicketId == id);
        }

        private async Task<bool> Insertar(Tickets ticket)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tickets.Add(ticket);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Tickets ticket)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tickets.Update(ticket);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Tickets ticket)
        {
            if (!await Existe(ticket.TicketId))
                return await Insertar(ticket);
            else
                return await Modificar(ticket);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var eliminado = await contexto.Tickets
                .Where(t => t.TicketId == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<Tickets?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets.AsNoTracking()
                .FirstOrDefaultAsync(t => t.TicketId == id);
        }

        public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets.AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
