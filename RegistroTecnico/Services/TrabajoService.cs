using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
	public class TrabajosService
	{
		private readonly Contexto _Contexto;

		public TrabajosService(Contexto Contexto) => _Contexto = Contexto;

		// Verifica si un trabajo existe según su ID
		public async Task<bool> Existe(int trabajosId)
		{
			return await _Contexto.Trabajos
				.AnyAsync(T => T.TrabajoId == trabajosId);
		}

		// Inserta un nuevo trabajo
		public async Task<bool> Insertar(Trabajos trabajo)
		{
			await _Contexto.Trabajos.AddAsync(trabajo);
			return await _Contexto.SaveChangesAsync() > 0;
		}

		// Modifica un trabajo existente
		public async Task<bool> Modificar(Trabajos trabajo)
		{
			_Contexto.Update(trabajo);
			return await _Contexto.SaveChangesAsync() > 0;
		}

		// Guarda un trabajo (inserta si no existe, modifica si ya existe)
		public async Task<bool> Guardar(Trabajos trabajo)
		{
			if (!await Existe(trabajo.TrabajoId))
				return await Insertar(trabajo);
			else
				return await Modificar(trabajo);
		}

		// Elimina un trabajo por su ID
		public async Task<bool> Eliminar(int trabajoId)
		{
			var trabajo = await _Contexto.Trabajos
				.FirstOrDefaultAsync(T => T.TrabajoId == trabajoId);

			if (trabajo != null)
			{
				_Contexto.Trabajos.Remove(trabajo);
				return await _Contexto.SaveChangesAsync() > 0;
			}

			return false;
		}

		// Busca un trabajo por su ID e incluye los nombres del cliente y técnico
		public async Task<Trabajos?> Buscar(int trabajosId)
		{
			return await _Contexto.Trabajos
				.AsNoTracking()
				.Include(T => T.Clientes) // Incluye el cliente
				.Include(T => T.Tecnicos) // Incluye el técnico
				.FirstOrDefaultAsync(T => T.TrabajoId == trabajosId);
		}

		// Lista trabajos basándose en un criterio, incluyendo el nombre del cliente y técnico
		public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
		{
			return await _Contexto.Trabajos
				.AsNoTracking()
				.Include(T => T.Clientes)  // Incluye el cliente
				.Include(T => T.Tecnicos)  // Incluye el técnico
				.Where(criterio)
				.ToListAsync();
		}
	}
}

