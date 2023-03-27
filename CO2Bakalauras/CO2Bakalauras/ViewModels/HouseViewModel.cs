using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using CO2Bakalauras.Views;

namespace CO2Bakalauras.ViewModels
{
    //Model Mileage Weight

    [QueryProperty("Model", "model")]
    [QueryProperty("Mileage", "mileage")]
    [QueryProperty("Weight", "weight")]

    public class HouseViewModel : BindableObject
    {
        private bool areaVisible;
        private bool electricityVisible;
        private bool waterVisible;
        private bool typeVisible;
        private bool gasVisible;
        private byte stage = 0;
        public bool AreaVisible {
            get { return areaVisible; }
            set 
            { 
                areaVisible = value;
                OnPropertyChanged();
            }
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
        public bool TypeVisible
        {
            get { return typeVisible; }
            set
            {
                typeVisible = value;
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
        public string Model { get; set; }
        public string Mileage { get; set; }
        public string Weight { get; set; }
        public string Area { get; set; }
        public string Electricity { get; set; }
        public string Water { get; set; }
        public string Selected { get; set; }
        public string Gas { get; set; }

        public HouseViewModel()
        {
            AreaVisible = true;
            ElectricityVisible = false;
            WaterVisible = false;
            TypeVisible = false;
            GasVisible = false;
            NextCommand = new Command(NextFunction);
        }

        private async void NextFunction()
        {
            WebService webService = new WebService();
            Sanaudos sanaudos = new Sanaudos();
            if(stage == 0)
            {
                AreaVisible = false;
                ElectricityVisible = true;
                stage++;
            }
            else if (stage == 1)
            {
                ElectricityVisible = false;
                WaterVisible= true;
                stage++;
            }
            else if (stage == 2)
            {
                WaterVisible = false;
                TypeVisible= true;
                stage++;
            }
            else if (stage == 3) 
            {
                if (Selected == "Dujos")
                {
                    TypeVisible = false;
                    GasVisible = true;
                    stage++;
                }
                else
                {
                    Gas = null;
                    stage++;
                }
            }
            else if(stage == 4)
            {
                sanaudos.VARTOTOJO_ID = vartotojas.VARTOTOJO_ID;
                sanaudos.AUTOMOBILIO_RIDA = 0;
                sanaudos.ELEKTROS_SANAUDOS = 0;
                sanaudos.VANDENS_SANAUDOS = 0;
                sanaudos.DUJU_SANAUDOS = 0;
                sanaudos.DATA = DateTime.Now;

                await webService.CreateUsage(sanaudos);
                List<Sanaudos> sanaudosActual = await webService.GetUserUsage(vartotojas.VARTOTOJO_ID);
                sanaudos = sanaudosActual.Last();
                
                if (Model.Length !=0 || Mileage.Length != 0 || Weight.Length != 0)
                {
                    Automobilis automobilis = new Automobilis()
                    {
                        SANAUDU_ID = sanaudos.SANAUDU_ID,
                        AUTOMOBILIO_MARKE = Model,
                        RIDA = int.Parse(Mileage),
                        SVORIS = decimal.Parse(Weight)
                    };
                    await webService.CreateCar(automobilis);
                }
                Butas butas = new Butas()
                {
                    SANAUDU_ID = sanaudos.SANAUDU_ID,
                    PLOTAS = decimal.Parse(Area),
                    SILDYMO_TIPAS = Selected,
                    PIRMINES_ELEKTROS_SANAUDOS = decimal.Parse(Electricity),
                    PIRMINES_VANDENS_SANAUDOS = decimal.Parse(Water),

                };
                if (Gas != null)
                    butas.PIRMINES_DUJU_SANAUDOS = decimal.Parse(Gas);
                else
                    butas.PIRMINES_DUJU_SANAUDOS = -1;
                await webService.CreateHouse(butas);
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
        }
    }
}
