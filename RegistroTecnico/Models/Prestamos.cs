using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using RegistroTecnico.Models;

public class Prestamos
{
    [Key]
    public int PrestamoId { get; set; }

    [Required(ErrorMessage = "El ClienteId es requerido")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El monto es requerido")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "La cantidad de cuotas es requerida")]
    public int CantidadCuotas { get; set; }
    public string Concepto { get; set; }
    public DateTime Fecha { get; set; }

    public decimal Balance { get; set; }

    [ForeignKey("ClienteId")]
    public Clientes Cliente { get; set; }

    public ICollection<PrestamosDetalle> PrestamosDetalles { get; set; } = new List<PrestamosDetalle>();
}

public class PrestamosDetalle
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PrestamoId { get; set; }

    public int CuotaNo { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Valor { get; set; }
    public decimal Balance { get; set; }

    [ForeignKey("PrestamoId")]
    public Prestamos Prestamo { get; set; }
}