using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clientes.Data.Models;

public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El Nombre es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten Numeros")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "La direccion es obligatoria")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "El RNC es obligatorio.")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "El RNC debe contener 9 dígitos.")]
    public string? RNC { get; set; }

    [Required(ErrorMessage = "El límite de crédito es obligatorio.")]
    [Range(0, 1000000, ErrorMessage = "El límite de crédito debe estar entre 0 y 1,000,000.")]
    public decimal LimiteCredito { get; set; }


}
