using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{

    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }
        public string? Nombres {get; set; }
        public double SueldoHora { get; set;}
    }  
}

