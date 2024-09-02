using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{

    public class Tecnicos
    {
        [Key]
        public int tecniCold { get; set; }
        public string? nombre {get; set; }
        public double sueldoHora { get; set;}
    }  
}

