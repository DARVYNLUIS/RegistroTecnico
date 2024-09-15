using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
    public class ClientesService
    {
        private readonly Contexto _Contexto;
        public ClientesService(Contexto Contexto) => _Contexto = Contexto;

        // Verifica si un cliente existe según su nombre
        public async Task<bool> Existe(string nombre)
        {
            return await _Contexto.Clientes
                .AnyAsync(C => C.Nombres == nombre);
        }

        // Inserta un nuevo cliente
        public async Task<bool> Insertar(Clientes cliente)
        {
            await _Contexto.Clientes.AddAsync(cliente);
            return await _Contexto.SaveChangesAsync() > 0;
        }

        // Modifica un cliente existente
        public async Task<bool> Modificar(Clientes cliente)
        {
            _Contexto.Update(cliente);
            return await _Contexto.SaveChangesAsync() > 0;
        }

        // Guarda un cliente, inserta si no existe, modifica si ya existe
        public async Task<bool> Guardar(Clientes cliente)
        {
            if (!await Existe(cliente.Nombres))
                return await Insertar(cliente);
            else
                return await Modificar(cliente);
        }

        // Elimina un cliente por su ID
        public async Task<bool> Eliminar(int ClienteId)
        {
            var clientes = await _Contexto.Clientes
                .Where(C => C.ClienteId == ClienteId)
                .ExecuteDeleteAsync();
            return clientes > 0;
        }

        // Busca un cliente por su ID
        public async Task<Clientes?> Buscar(int ClienteId)
        {
            return await _Contexto.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(C => C.ClienteId == ClienteId);
        }

        // Lista clientes basándose en un criterio
        public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
        {
            return await _Contexto.Clientes
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
