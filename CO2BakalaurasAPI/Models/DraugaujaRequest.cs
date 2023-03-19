namespace CO2BakalaurasAPI.Models
{
    public class DraugaujaRequest
    {
        public int DRAUGO_ID { get; set; }
        public int PIRMO_DRAUGO_ID { get; set;}
        public int ANTRO_DRAUGO_ID { get; set;}
        public byte DRAUGO_LYGIS { get; set;}
        public byte DRAUGO_TASKU_SKAICIUS { get; set;}
        public bool PATVIRTINTAS { get; set;}

    }
}
