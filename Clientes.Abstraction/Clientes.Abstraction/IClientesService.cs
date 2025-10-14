using System.Linq.Expressions;
using Clientes.Data.Models;

namespace ClienteAPI.Abstractions;

public interface IClientesService
{
    Task<bool> Guardar(Cliente cliente);
    Task<bool> Eliminar(int id);
    Task<Cliente?> Buscar(int id);
    Task<List<Cliente>> Listar(Expression<Func<Cliente, bool>> criterio);
}
