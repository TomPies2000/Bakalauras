using CO2Bakalauras.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CO2Bakalauras.Services
{
    public static class PointsLogic
    {
        public static async Task ManagePoints(Statistika statistika, short taskai)
        {
            statistika.TASKU_SUMA += taskai;
            if (statistika.TASKU_SUMA >= ToNextLevel(statistika))
            {
                statistika.TASKU_SUMA -= ToNextLevel(statistika);
                statistika.LYGIS++;
            }


            if (statistika.TASKU_SUMA < 0)
            {
                if (statistika.LYGIS > 1)
                {
                    statistika.LYGIS--;
                    statistika.TASKU_SUMA += ToNextLevel(statistika);
                }
                else
                    statistika.TASKU_SUMA = 0;
            }
            if(statistika.LYGIO_PAVADINIMAS != "Herojus")
                statistika = ManageLevelNames(statistika);

            WebService web = new WebService();
            await web.UpdateStatistic(statistika);
        }

        public static Statistika ManageLevelNames(Statistika statistika)
        {
            if (statistika.LYGIS == 1)
            {
                statistika.LYGIO_PAVADINIMAS = "Naujokas";
            }
            else if (statistika.LYGIS == 2)
            {
                statistika.LYGIO_PAVADINIMAS = "Pradedantysis";
            }
            else if (statistika.LYGIS == 3)
            {
                statistika.LYGIO_PAVADINIMAS = "Darantis pastangas";
            }
            else if (statistika.LYGIS == 4)
            {
                statistika.LYGIO_PAVADINIMAS = "Profesionalas";
            }
            else if (statistika.LYGIS == 5)
            {
                statistika.LYGIO_PAVADINIMAS = "Legenda";
            }
            return statistika;
        }

        public static Statistika AddHeroStatus(Statistika statistika)
        {
            statistika.LYGIO_PAVADINIMAS = "Herojus";
            return statistika;
        }

        public static short ToNextLevel(Statistika statistika)
        {
            if (statistika.LYGIS == 1)
            {
                return 50;
            }
            else if (statistika.LYGIS == 2)
            {
                return 100;
            }
            else if (statistika.LYGIS == 3)
            {
                return 250;
            }
            else if (statistika.LYGIS == 4)
            {
                return 500;
            }
            else
            {
                return 32767;
            }

        }


    }
}
