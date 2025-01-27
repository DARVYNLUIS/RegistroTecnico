using System.ComponentModel.DataAnnotations;
namespace RegistroTecnico.Models
{
    public class Ciudad
    {
        [Key]
        public int CiudadId { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]

        public string Nombre { get; set; } = null!;

    }
}