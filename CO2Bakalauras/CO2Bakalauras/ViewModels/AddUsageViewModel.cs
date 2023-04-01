﻿using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class AddUsageViewModel:BaseViewModel
    {
        public Command AddCarCommand { get; set; }

        private bool _activityIndicator;
        private string currentMileage;
        public Vartotojas vartotojas = ((App)App.Current).CurrentUser;
        public string Mileage { get; set; }
        public bool ActivityIndicator
        {
            get
            {
                return _activityIndicator;
            }
            set
            {
                _activityIndicator = value;
                OnPropertyChanged();
            }
        }
        public string CurrentMileage
        {
            get { return currentMileage; }
            set
            {
                currentMileage = value;
                OnPropertyChanged();
            }
        }
        public AddUsageViewModel()
        {
            
            //ToListCommand = new Command(async () => { await Load(); });
            Load();
            AddCarCommand = new Command(AddCar);
        }

        async void Load()
        {
            WebService webService = new WebService();

            Sanaudos sanaudos = await webService.GetUserUsage(vartotojas.VARTOTOJO_ID);

            Automobilis auto = await webService.GetCarByUsageId(sanaudos.SANAUDU_ID);
            CurrentMileage = "Prieš mėnesį automobilio rida buvo - " + auto.RIDA + " km";
        }
        async void AddCar()
        {
            await Shell.Current.GoToAsync($"/{nameof(AddUsagePage2)}?mileage={Mileage}");
        }
    }
}
