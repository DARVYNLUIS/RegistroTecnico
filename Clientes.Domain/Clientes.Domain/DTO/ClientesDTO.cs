namespace Clientes.Domain.DTO
{
    public class ClientesDto
    {
        public int ClienteId { get; set; } 

        public string? Nombres { get; set; }

        public string? Direccion { get; set; }

        public string? RNC { get; set; }

        public decimal LimiteCredito { get; set; }

        public DateTime FechaIngreso { get; set; }
    }
}
