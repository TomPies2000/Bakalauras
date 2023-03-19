using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CO2BakalaurasAPI.Data
{
    public class Vartotojas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VARTOTOJO_ID { get; set; }
        public string? VARTOTOJO_EMAIL { get; set; }
        public string? VARTOTOJO_VARDAS { get; set; }
        public string? VARTOTOJO_PAVARDE { get; set; }
        public string? VARTOTOJO_SLAPTAZODIS { get; set; }
        public string? PRISIJUNGIMO_VARDAS { get; set; }
    }
}
