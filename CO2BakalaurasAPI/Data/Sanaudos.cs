using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CO2BakalaurasAPI.Data
{
    public class Sanaudos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SANAUDU_ID { get; set; }
        public int VARTOTOJO_ID { get; set; }
        public int AUTOMOBILIO_RIDA { get; set; }
        public decimal ELEKTROS_SANAUDOS { get; set; }
        public decimal VANDENS_SANAUDOS { get; set; }
        public decimal DUJU_SANAUDOS { get; set; }
        public DateTime DATA { get; set; }
    }
}
