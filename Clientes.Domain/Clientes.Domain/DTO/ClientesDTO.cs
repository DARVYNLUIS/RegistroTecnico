using System.ComponentModel.DataAnnotations;

namespace Clientes.Domain.DTO
{
    public class ClienteCreateDto
    {
        public string Nombres { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string RNC { get; set; } = null!;
        public decimal LimiteCredito { get; set; }
        public DateTime FechaIngreso { get; set; } = DateTime.Now;
    }
}

