using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace CO2Bakalauras.ViewModels
{
    public class MileageViewModel : BaseViewModel
    {
        public Command AddCarCommand { get; set; }
        public Command WithoutCarCommand { get; set; }
        private bool _activityIndicator;
        public string Model { get; set; }
        public string Mileage { get; set; }
        public string Weight { get; set; }
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
        public MileageViewModel()
        {
            AddCarCommand = new Command(OnAddCarClicked);
            WithoutCarCommand = new Command(OnWithoutCarClicked);
        }
        private async void OnAddCarClicked ()
        {

            if (Model == null || Model.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite automobilio markę", "Ok");
                return;
            }
            else if (Mileage == null || int.Parse(Mileage) == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite automobilio ridą", "Ok");
                return;
            }
            else if (Weight == null ||decimal.Parse(Weight) == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite automobilio svorį", "Ok");
                return;
            }
            else
            {
                ActivityIndicator = true;
                await Shell.Current.GoToAsync($"//{nameof(HousePage)}?model={Model}&mileage={Mileage}&weight={Weight}");
                ActivityIndicator = false;
            }

        }
        private async void OnWithoutCarClicked()
        {
            Model = null;
            Mileage = null;
            Weight = null;
            await Shell.Current.GoToAsync($"//{nameof(HousePage)}?model={Model}&mileage={Mileage}&weight={Weight}");

        }
    }
}
