namespace CO2BakalaurasAPI.Models
{
    public class CO2Request
    {
        public int CO2_ID { get; set; }
        public int ADMINISTRATORIAUS_ID { get; set; }
        public int ELEKTROS_CO2 { get; set; }
        public int VANDENS_CO2 { get; set; }
        public int DUJU_CO2 { get; set; }
        public DateTime PASKUTINIS_ATNAUJUNIMAS { get; set; }
    }
}
