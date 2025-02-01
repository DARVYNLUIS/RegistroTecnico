using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class Tickets
{
    [Key]
    public int TicketId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "La prioridad es obligatoria")]
    [Range(1, 5, ErrorMessage = "La prioridad debe estar entre 1 y 5")]
    public int Prioridad { get; set; }

    [Required(ErrorMessage = "El ClienteId es obligatorio")]
    public int ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    public virtual Clientes Clientes { get; set; } = null!;

    [Required(ErrorMessage = "El asunto es obligatorio")]
    [StringLength(100, ErrorMessage = "El asunto no puede tener más de 100 caracteres")]
    public string? Asunto { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    public string? Descripcion { get; set; }

    [Range(0, 1000, ErrorMessage = "El tiempo invertido debe estar entre 0 y 1000 horas")]
    public int TiempoInvertido { get; set; }

    [Required(ErrorMessage = "El TecnicoId es obligatorio")]
    public int TecnicoId { get; set; }

    [ForeignKey("TecnicoId")]
    public virtual Tecnicos Tecnicos { get; set; } = null!;
}
