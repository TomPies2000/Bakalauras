using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CO2BakalaurasAPI.Data
{
    public class Atlieka
    {
        [Key]
        public int VARTOTOJO_ID { get; set; }
        public int UZDUOTIES_ID { get; set; }
    }
}
