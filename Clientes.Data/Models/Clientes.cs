using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clientes.Data.Models;

public class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int ClienteId { get; set; }

    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El Nombre es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten números")]
    public string Nombres { get; set; } = null!;

    [Required(ErrorMessage = "La dirección es obligatoria")]
    public string Direccion { get; set; } = null!;

    [Required(ErrorMessage = "El RNC es obligatorio.")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "El RNC debe contener 9 dígitos.")]
    public string RNC { get; set; } = null!;

    [Required(ErrorMessage = "El límite de crédito es obligatorio.")]
    [Range(0, 1000000, ErrorMessage = "El límite de crédito debe estar entre 0 y 1,000,000.")]
    public decimal LimiteCredito { get; set; }
}
