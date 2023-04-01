using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private Chart chart;
        public Chart ChartView {
            get
            {
                return chart;
            }
            set
            {
                chart = value;
                OnPropertyChanged();
            }
        }
        public Command AddUsageCommand { get; set; }
        public string coSum;
        public String CoSum {
            get 
            {
                return coSum; 
            }
            set 
            {
                coSum = value;
                OnPropertyChanged();
            }
        }

        Vartotojas vartotojas = ((App)App.Current).CurrentUser;
        public WebService webService = new WebService();
        public Sanaudos sanaudos = new Sanaudos();
        public MainViewModel()
        {
            SanaudosView();
            AddUsageCommand = new Command(AddUsage);
        }
        public readonly ChartEntry[] entries;

        //ToDo Padidinti chart srifta
        private async void SanaudosView()
        {
            WebService webService = new WebService();
            List <Sanaudos> allSanaudos = await webService.GetUserUsage(vartotojas.VARTOTOJO_ID);
            sanaudos = allSanaudos.OrderByDescending(o => o.DATA).Take(1).FirstOrDefault();
            List<CO2> co = await webService.GetCo2();
            CO2 co2 = co.OrderByDescending(o => o.PASKUTINIS_ATNAUJINIMAS).Take(1).FirstOrDefault();

            decimal auto = (co2.AUTOMOBILIO_CO2 * sanaudos.AUTOMOBILIO_RIDA)/1000;
            decimal electr = (co2.ELEKTROS_CO2 * sanaudos.ELEKTROS_SANAUDOS)/ 1000;
            decimal water = (co2.VANDENS_CO2 * sanaudos.VANDENS_SANAUDOS) / 1000;
            decimal gas = (co2.DUJU_CO2 * sanaudos.DUJU_SANAUDOS) / 1000;
            decimal sum = (co2.AUTOMOBILIO_CO2 * sanaudos.AUTOMOBILIO_RIDA + co2.ELEKTROS_CO2 * sanaudos.ELEKTROS_SANAUDOS + co2.VANDENS_CO2 + sanaudos.VANDENS_SANAUDOS + co2.DUJU_CO2 * sanaudos.DUJU_SANAUDOS) / 1000;
            
            //ToDo Isaiskinti kaip perkelti CoSum i Xaml
            CoSum = sum.ToString() + " Kg";

            List<ChartEntry> entries = new List<ChartEntry>
            {
                new ChartEntry((float)auto)
                {
                    Color = SKColor.Parse("#FA1943"),
                    Label = "Automobilis",
                    ValueLabel = auto.ToString()
                },
                new ChartEntry((float)electr)
                {
                    Color = SKColor.Parse("#FF1203"),
                    Label = "Elektra",
                    ValueLabel = electr.ToString()
                },
                new ChartEntry((float)water)
                {
                    Color = SKColor.Parse("#FF1043"),
                    Label = "Vanduo",
                    ValueLabel = water.ToString()
                }
            };
            Butas butas = await webService.GetHouseByUsageId(sanaudos.SANAUDU_ID);
            if (butas.PIRMINES_DUJU_SANAUDOS != -1)
            {
                entries.Add(new ChartEntry((float)gas)
                {
                    Color = SKColor.Parse("#FF1973"),
                    Label = "Dujos",
                    
                    ValueLabel = gas.ToString()
                });
            }
            ChartView = new BarChart { Entries = entries, LabelTextSize = (float)Convert.ToDouble(Device.GetNamedSize(NamedSize.Large, typeof(LabelMode))) };
        }

        private async void AddUsage()
        {
            Automobilis auto = await webService.GetCarByUsageId(sanaudos.SANAUDU_ID);
            if (auto != null)
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}/{nameof(AddUsagePage)}");
            }
            else
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}/{nameof(AddUsagePage2)}");
        }
    }
}
