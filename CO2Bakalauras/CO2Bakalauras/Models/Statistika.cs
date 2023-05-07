
using System;

namespace CO2Bakalauras.Models
{
    public class Statistika
    {

        public int VARTOTOJO_ID { get; set; }
        public int STATISTIKOS_ID { get; set; }
        public byte LYGIS { get; set; }
        public string LYGIO_PAVADINIMAS { get; set; }
        public Int16 TASKU_SUMA { get; set; } //SMALLINT IS SHORT
        public DateTime LAIKOTARPIS { get; set; }

    }


}
