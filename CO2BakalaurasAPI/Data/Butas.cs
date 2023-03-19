using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CO2BakalaurasAPI.Data
{
    public class Butas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BUTO_ID { get; set; }
        public int SANAUDU_ID { get; set; }
        public decimal PLOTAS { get; set; }
        public string? SILDYMO_TIPAS { get; set; }
        public decimal PIRMINES_ELEKTROS_SANAUDOS { get; set; }
        public decimal PIRMINES_VANDENS_SANAUDOS { get; set; }
        public decimal PIRMINES_DUJU_SANAUDOS { get; set; }
    }
}
