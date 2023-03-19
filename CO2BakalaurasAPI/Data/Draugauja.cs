using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CO2BakalaurasAPI.Data
{
    public class Draugauja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DRAUGO_ID { get; set; }
        public int PIRMO_DRAUGO_ID { get; set; }
        public int ANTRO_DRAUGO_ID { get; set; }
        public byte DRAUGO_LYGIS { get; set; }
        public byte DRAUGO_TASKU_SKAICIUS { get; set; }
        public bool PATVIRTINTAS { get; set; }
    }
}
