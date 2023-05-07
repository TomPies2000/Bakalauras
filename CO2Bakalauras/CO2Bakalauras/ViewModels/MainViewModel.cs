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
        public Command CheckUsageCommand { get; set; }

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
        public string text;
        public String Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        readonly Vartotojas vartotojas = ((App)App.Current).CurrentUser;
        public WebService webService = new WebService();
        public Sanaudos sanaudos = new Sanaudos();
        List<Sanaudos> allSanaudos = new List<Sanaudos>();
        public MainViewModel()
        {
            AddUsageCommand = new Command(AddUsage);
            CheckUsageCommand = new Command(CheckUsage);
        }
        public readonly ChartEntry[] entries;

        //ToDo patikrinti ar si menesi buvo pildoma sanaudu deklaracija - Jeigu ne issiusti priminima
        //Istorija
        //Draugo statistika
        //Iki sekancio lygio kiek tasku
        //Delete Vartotojas
        //Tips on statistic page
        //Didžiausios/mažiausios sąnaudos

        private async void SanaudosView()
        {
            WebService webService = new WebService();
            allSanaudos = await webService.GetUserUsage(vartotojas.VARTOTOJO_ID);
            sanaudos = allSanaudos.OrderByDescending(o => o.DATA).Take(1).FirstOrDefault();
            List<CO2> co = await webService.GetCo2();
            CO2 co2 = co.OrderByDescending(o => o.PASKUTINIS_ATNAUJINIMAS).Take(1).FirstOrDefault();


            if (allSanaudos.Count() == 1)
            {
                Text = "Sekantį mėnesį užpildykite sąnaudų informaciją";
                CoSum = "";
            }
            else
            {
                Text = "Praeitą mėnesį sugeneravote: ";
                decimal auto = ((decimal)co2.AUTOMOBILIO_CO2 * (decimal)sanaudos.AUTOMOBILIO_RIDA) / 1000;
                decimal electr = ((decimal)co2.ELEKTROS_CO2 * (decimal)sanaudos.ELEKTROS_SANAUDOS) / 1000;
                decimal water = ((decimal)co2.VANDENS_CO2 * (decimal)sanaudos.VANDENS_SANAUDOS) / 1000;
                decimal gas = ((decimal)co2.DUJU_CO2 * (decimal)sanaudos.DUJU_SANAUDOS) / 1000;
                decimal sum = (auto + electr + water + gas);

                CoSum = sum.ToString() + " Kg CO2";

                List<ChartEntry> entries = new List<ChartEntry>
                {

                    new ChartEntry((float)electr)
                    {
                        Color = SKColor.Parse("#fcfc03"),
                        Label = "Elektra",
                        ValueLabel = electr.ToString()
                    },
                    new ChartEntry((float)water)
                    {
                        Color = SKColor.Parse("#03fc0f"),
                        Label = "Vanduo",
                        ValueLabel = water.ToString()
                    }
                };
                Butas butas = await webService.GetHouseByUsageId(sanaudos.SANAUDU_ID);
                Automobilis automobilis = await webService.GetCarByUsageId(sanaudos.SANAUDU_ID);
                if (butas.PIRMINES_DUJU_SANAUDOS != -1)
                {
                    entries.Add(new ChartEntry((float)gas)
                    {
                        Color = SKColor.Parse("#fc0307"),
                        Label = "Dujos",

                        ValueLabel = gas.ToString()
                    });
                }
                if (automobilis != null)
                {
                    entries.Add(new ChartEntry((float)auto)
                    {
                        Color = SKColor.Parse("#0328fc"),
                        Label = "Automobilis",
                        ValueLabel = auto.ToString()
                    });
                }
                ChartView = new BarChart { Entries = entries, LabelTextSize = 50, BackgroundColor = SKColors.Transparent };
            }
        }

        private async void AddUsage()
        {
            Automobilis auto = await webService.GetCarByUsageId(sanaudos.SANAUDU_ID);
            DateTime date = DateTime.Now;

            if(date.Month == sanaudos.DATA.Month)
            {
                if(allSanaudos.Count() == 1)
                {
                    await Application.Current.MainPage.DisplayAlert("Oops..", "Sanaudų formą galite užpildyti tik sekantį mėnesį", "Ok");
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Oops..", "Šį mėnesį jau pildėte sanaudų formą", "Ok");
                    return;
                }
            }

            if (auto != null)
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}/{nameof(AddUsagePage)}");
            }
            else
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}/{nameof(AddUsagePage2)}");

            SanaudosView();
        }

        async void CheckUsage()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}/{nameof(CheckUsagePage)}");
        }


        public void VModelActive()
        {
            SanaudosView(); 
        }



    }
}
