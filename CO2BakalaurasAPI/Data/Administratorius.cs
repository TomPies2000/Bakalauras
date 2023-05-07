using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CO2BakalaurasAPI.Data
{
    public class Administratorius
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ADMINISTRATORIAUS_ID { get; set; }
        public int VARTOTOJO_ID { get; set; }
        public int TELEFONO_NUMERIS { get; set; }
        public string? ADRESAS { get; set; }
        public Int64 ASMENS_KODAS { get; set; }
        public bool BUSENA { get; set; }
    }
}
