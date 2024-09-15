using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RegistroTecnico.Models;

public class Tecnicos
{
    [Key]  
    public string Nombres { get; set; }
    public decimal SueldoHora { get; set; }
    public string TipoTecnico { get; set; }
    public int TecnicoId { get; set; }
	public int TrabajoId { get; internal set; }
	public int TrabajosId { get; internal set; }
}
public class Clientes
{
    [Key]
    public int ClienteId { get; set; }
    public string WhatsApp { get; set; }
    public string Nombres { get; set; }
}
public class Trabajos
{
    [Key]
    public int TrabajoId { get; set; }
    public DateTime Fecha { get; set; }
    public int ClienteId { get; set; }
    public Clientes Clientes { get; set; }  // Relación con Cliente
    public int TecnicoId { get; set; }
    public Tecnicos Tecnicos { get; set; }  // Relación con Técnico
    public string Descripcion {  get; set; }
    public decimal Monto { get; set; }
}




