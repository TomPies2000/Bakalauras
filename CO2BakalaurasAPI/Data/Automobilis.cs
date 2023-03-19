using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CO2BakalaurasAPI.Data
{
    public class Automobilis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AUTOMOBILIO_ID { get; set; }
        public int SANAUDU_ID { get; set; }
        public string? AUTOMOBILIO_MARKE { get; set; }
        public int RIDA { get; set; }
        public decimal SVORIS { get; set; }
    }
}
