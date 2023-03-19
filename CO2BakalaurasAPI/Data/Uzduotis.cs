﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CO2BakalaurasAPI.Data

{
    public class Uzduotis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UZDUOTIES_ID { get; set; }
        public int ADMINISTRATORIAUS_ID { get; set; }
        public string? PAVADINIMAS { get; set; }
        public byte TASKU_SKAICIUS { get; set; }
        public string? KATEGORIJA { get; set; }
        public DateTime PRIMINIMO_LAIKAS { get; set; }
    }
}
