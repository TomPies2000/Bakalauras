using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CO2BakalaurasAPI.Data
{
    public class CO2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CO2_ID { get; set; }
        public int ADMINISTRATORIAUS_ID { get; set; }
        public int AUTOMOBILIO_CO2 { get; set; }
        public int ELEKTROS_CO2 { get; set; }
        public int VANDENS_CO2 { get; set; }
        public int DUJU_CO2 { get; set; }
        public DateTime PASKUTINIS_ATNAUJINIMAS { get; set; }
    }
}
