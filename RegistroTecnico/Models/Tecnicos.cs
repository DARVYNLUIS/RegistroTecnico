using System.ComponentModel.DataAnnotations;
namespace RegistroTecnico.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]

        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public float SueldoHora { get; set; }
    }
}