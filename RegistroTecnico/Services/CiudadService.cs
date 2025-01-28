using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
    public class CiudadService
    {
        private readonly Contexto _context;
        public CiudadService(Contexto contexto) => _context = contexto;

        public async Task<bool> Guardar(Ciudad ciudad)
        {
            if (!await Existe(ciudad.CiudadId))
                return await Insertar(ciudad);
            else
                return await Modificar(ciudad);
        }

        public async Task<bool> Insertar(Ciudad ciudad)
        {
            _context.Ciudad.Add(ciudad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Ciudad ciudad)
        {
            _context.Ciudad.Update(ciudad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Existe(int id)
        {
            return await _context.Ciudad.AnyAsync(c => c.CiudadId == id);
        }

        public async Task<bool> Existe(string? nombre, int? id = null)
        {
            return await _context.Ciudad
                .AnyAsync(c => c.Nombre == nombre && (!id.HasValue || c.CiudadId != id));
        }

        public async Task<bool> Eliminar(int id)
        {
            var ciudad = await _context.Ciudad.FindAsync(id);
            if (ciudad == null)
                return false;

            _context.Ciudad.Remove(ciudad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Ciudad?> Buscar(int id)
        {
            return await _context.Ciudad
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CiudadId == id);
        }

        public async Task<List<Ciudad>> Listar(Expression<Func<Ciudad, bool>> criterio)
        {
            return await _context.Ciudad
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<List<Ciudad>> ListarTodo()
        {
            return await _context.Ciudad
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

