using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class SistemaService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public SistemaService(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AnyAsync(s => s.SistemaId == id);
    }

    private async Task<bool> Insertar(Sistemas sistema)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Sistemas.Add(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Sistemas sistema)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Sistemas.Update(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Sistemas sistema)
    {
        if (!await Existe(sistema.SistemaId))
            return await Insertar(sistema);
        else
            return await Modificar(sistema);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var eliminados = await contexto.Sistemas
            .Where(s => s.SistemaId == id)
            .ExecuteDeleteAsync();
        return eliminados > 0;
    }

    public async Task<Sistemas?> Buscar(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking()
            .FirstOrDefaultAsync(s => s.SistemaId == id);
    }

    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Sistemas>> ObtenerLista()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking().ToListAsync();
    }
    public async Task<bool> ExistePorDescripcion(string descripcion)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AnyAsync(s => s.Descripcion == descripcion);
    }


}