using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    [QueryProperty("Model", "model")]
    [QueryProperty("Mileage", "mileage")]
    [QueryProperty("Weight", "weight")]

    public class AddUsage2ViewModel : BaseViewModel
    {
        private bool electricityVisible;
        private bool waterVisible;
        private bool gasVisible;
        private byte stage = 0;
        private string currentElectricity;
        private string currentWater;
        private string currentGas;

        public AddUsage2ViewModel()
        {
            ElectricityVisible = true;
            WaterVisible = false;
            GasVisible = false;
            NextCommand = new Command(NextFunction);
        }
        public bool ElectricityVisible
        {
            get { return electricityVisible; }
            set
            {
                electricityVisible = value;
                OnPropertyChanged();
            }
        }
        public bool WaterVisible
        {
            get { return waterVisible; }
            set
            {
                waterVisible = value;
                OnPropertyChanged();
            }
        }
        public bool GasVisible
        {
            get { return gasVisible; }
            set
            {
                gasVisible = value;
                OnPropertyChanged();
            }
        }
        public Command NextCommand { get; set; }
        Vartotojas vartotojas = ((App)App.Current).CurrentUser;
        public string Mileage { get; set; }
        public string Electricity { get; set; }
        public string Water { get; set; }
        public string Gas { get; set; }

        public string CurrentElectricity 
        {
            get { return currentElectricity; }
            set
            {
                currentElectricity = value;
                OnPropertyChanged();
            }
        }
        public string CurrentWater 
        {
            get { return currentWater; }
            set
            {
                currentWater = value;
                OnPropertyChanged();
            }
        }
        public string CurrentGas 
        {
            get { return currentGas; }
            set
            {
                currentGas = value;
                OnPropertyChanged();
            }
        }



        private async void NextFunction()
        {
            WebService webService = new WebService();
            Sanaudos sanaudos = new Sanaudos();
            Sanaudos senosSanaudos;
            List<Sanaudos> sanaudosList;
            sanaudosList = await webService.GetUserUsage(vartotojas.VARTOTOJO_ID);
            senosSanaudos = sanaudosList.OrderByDescending(o => o.DATA).Take(1).FirstOrDefault();

            Butas senasButas = await webService.GetHouseByUsageId(senosSanaudos.SANAUDU_ID);
            Automobilis senasAuto = await webService.GetCarByUsageId(senosSanaudos.SANAUDU_ID);

            CurrentElectricity = "Prieš mėnesį elektros sąnaudos buvo - " + senasButas.PIRMINES_ELEKTROS_SANAUDOS + " kvh";
            CurrentWater = "Prieš mėnesį vandens sąnaudos buvo - " + senasButas.PIRMINES_VANDENS_SANAUDOS + " kubų";
            CurrentGas = "Prieš mėnesį dujų sąnaudos buvo - " + senasButas.PIRMINES_DUJU_SANAUDOS + " kubų";

            if (stage == 0)
            {
                ElectricityVisible = false;
                WaterVisible = true;
                stage++;
            }
            else if (stage == 1)
            {
                if (senasButas.SILDYMO_TIPAS == "Dujos")
                {
                    WaterVisible = false;
                    GasVisible = true;
                    stage++;
                }
                else
                {
                    Gas = null;
                    stage++;
                }

            }
            else if (stage == 2)
            {
                sanaudos.VARTOTOJO_ID = vartotojas.VARTOTOJO_ID;
                sanaudos.AUTOMOBILIO_RIDA = 0;
                sanaudos.ELEKTROS_SANAUDOS = 0;
                sanaudos.VANDENS_SANAUDOS = 0;
                sanaudos.DUJU_SANAUDOS = 0;
                sanaudos.DATA = DateTime.Now;

                sanaudosList = null;

                await webService.CreateUsage(sanaudos);
                sanaudosList = await webService.GetUserUsage(vartotojas.VARTOTOJO_ID);
                sanaudos = sanaudosList.OrderByDescending(o => o.DATA).Take(1).FirstOrDefault();
                if (Mileage.Length != 0)
                {
                    int senaRida = senasAuto.RIDA;
                    senasAuto.SANAUDU_ID = sanaudos.SANAUDU_ID;
                    senasAuto.RIDA = int.Parse(Mileage);

                    sanaudos.AUTOMOBILIO_RIDA = senasAuto.RIDA - senaRida;
                    await webService.CreateCar(senasAuto);
                }

                if(Electricity.Length != 0 || Water.Length !=0) 
                {
                    //Atnaujiname sanaudu lentele
                    sanaudos.ELEKTROS_SANAUDOS = decimal.Parse(Electricity) - senasButas.PIRMINES_ELEKTROS_SANAUDOS;
                    sanaudos.VANDENS_SANAUDOS = decimal.Parse(Water) - senasButas.PIRMINES_VANDENS_SANAUDOS;
                    if (Gas != null)
                        sanaudos.DUJU_SANAUDOS = decimal.Parse(Gas) - senasButas.PIRMINES_DUJU_SANAUDOS;


                    senasButas.SANAUDU_ID = sanaudos.SANAUDU_ID;
                    senasButas.PIRMINES_ELEKTROS_SANAUDOS = decimal.Parse(Electricity);
                    senasButas.PIRMINES_VANDENS_SANAUDOS = decimal.Parse(Water);
                    if (Gas != null)
                        senasButas.PIRMINES_DUJU_SANAUDOS = decimal.Parse(Gas);
                    else
                        senasButas.PIRMINES_DUJU_SANAUDOS = -1;
                    await webService.CreateHouse(senasButas);
                }
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
        }
    }
}
