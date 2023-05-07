

using System;
namespace CO2Bakalauras.Models

{
    public class Uzduotis
    {

        public int UZDUOTIES_ID { get; set; }
        public int ADMINISTRATORIAUS_ID { get; set; }
        public string PAVADINIMAS { get; set; }
        public string APRASYMAS { get; set; }
        public byte TASKU_SKAICIUS { get; set; }
        public string KATEGORIJA { get; set; }
        public DateTime PRIMINIMO_LAIKAS { get; set; }
    }
}
