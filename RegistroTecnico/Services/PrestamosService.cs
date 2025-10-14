using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RegistroTecnico.Services
{
    public class PrestamosService
    {
        private readonly IDbContextFactory<Contexto> DbFactory;

        public PrestamosService(IDbContextFactory<Contexto> dbFactory)
        {
            this.DbFactory = dbFactory;
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Prestamos.AnyAsync(p => p.PrestamoId == id);
        }

        public async Task<bool> Insertar(Prestamos prestamo)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Prestamos.Add(prestamo);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Prestamos prestamo)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Prestamos.Update(prestamo);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Prestamos prestamo)
        {
            if (!await Existe(prestamo.PrestamoId))
                return await Insertar(prestamo);
            else
                return await Modificar(prestamo);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var prestamo = await contexto.Prestamos.FindAsync(id);
            if (prestamo == null) return false;

            contexto.Prestamos.Remove(prestamo);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Prestamos?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Prestamos
                .Include(p => p.Cliente) 
                .Include(p => p.PrestamosDetalles) 
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PrestamoId == id);
        }

        public async Task<List<Prestamos>> Listar(Expression<Func<Prestamos, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Prestamos
                .Include(p => p.Cliente) 
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<List<PrestamosDetalle>> CalcularCuotas(Prestamos prestamo)
        {
            var detalles = new List<PrestamosDetalle>();
            decimal tasaMensual = 0.05m; 
            decimal cuota = (prestamo.Monto * tasaMensual) / (1 - (decimal)Math.Pow(1 + (double)tasaMensual, -prestamo.CantidadCuotas));

            for (int i = 1; i <= prestamo.CantidadCuotas; i++)
            {
                var detalle = new PrestamosDetalle
                {
                    PrestamoId = prestamo.PrestamoId,
                    CuotaNo = i,
                    Fecha = DateTime.Now.AddMonths(i),
                    Valor = cuota,
                    Balance = cuota 
                };
                detalles.Add(detalle);
            }

            return detalles;
        }

        public async Task<bool> AplicarPago(int prestamoId, decimal montoPago)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var detalles = await contexto.PrestamosDetalles
                .Where(d => d.PrestamoId == prestamoId)
                .ToListAsync();

            foreach (var detalle in detalles)
            {
                if (montoPago <= 0) break;

                if (detalle.Balance > 0)
                {
                    if (montoPago >= detalle.Balance)
                    {
                        montoPago -= detalle.Balance;
                        detalle.Balance = 0; 
                    }
                    else
                    {
                        detalle.Balance -= montoPago; 
                        montoPago = 0; 
                    }
                }
            }

            contexto.PrestamosDetalles.UpdateRange(detalles);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<List<Prestamos>> ObtenerLista()
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Prestamos
                .Include(p => p.Cliente) 
                .Include(p => p.PrestamosDetalles) 
                .AsNoTracking()
                .ToListAsync();
        }
    }
}