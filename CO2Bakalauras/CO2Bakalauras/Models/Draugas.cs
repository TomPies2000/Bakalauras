using System;
using System.Collections.Generic;
using System.Text;

namespace CO2Bakalauras.Models
{
    class Draugas
    {
        public Vartotojas Vartotojas { get; set; }
        public Statistika Statistika { get; set; }

        public Draugas(Vartotojas vartotojas, Statistika statistika)
        {
            this.Vartotojas = vartotojas;
            this.Statistika = statistika;
        }


    }
}
