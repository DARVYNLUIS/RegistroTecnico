using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ClienteAPI.Abstractions;
using Clientes.Data.DAL;
using Clientes.Data.Models;

namespace ClienteAPI.Services;

public class ClientesService : IClientesService
{
    private readonly Contexto _context;

    public ClientesService(Contexto context)
    {
        _context = context;
    }

    public async Task<bool> Guardar(Cliente cliente)
    {
        if (!await _context.Clientes.AnyAsync(c => c.ClienteId == cliente.ClienteId))
        {
            _context.Clientes.Add(cliente);
        }
        else
        {
            _context.Clientes.Update(cliente);
        }
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return false;
        _context.Clientes.Remove(cliente);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Cliente?> Buscar(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<List<Cliente>> Listar(Expression<Func<Cliente, bool>> criterio)
    {
        return await _context.Clientes.Where(criterio).ToListAsync();
    }
}
