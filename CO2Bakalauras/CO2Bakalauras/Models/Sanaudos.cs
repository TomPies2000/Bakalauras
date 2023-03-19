

using System;

namespace CO2Bakalauras.Models
{
    public class Sanaudos
    {

        public int SANAUDU_ID { get; set; }
        public int VARTOTOJO_ID { get; set; }
        public int AUTOMOBILIO_RIDA { get; set; }
        public decimal ELEKTROS_SANAUDOS { get; set; }
        public decimal VANDENS_SANAUDOS { get; set; }
        public decimal DUJU_SANAUDOS { get; set; }
        public DateTime DATA { get; set; }
    }
}
