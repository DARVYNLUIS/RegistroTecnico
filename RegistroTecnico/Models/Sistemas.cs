using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class Sistemas
{
    [Key]
    public int SistemaId { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "La complejidad es obligatoria")]
    [Range(1, 10, ErrorMessage = "La complejidad debe estar entre 1 y 10.")]
    public int Complejidad { get; set; }
}
