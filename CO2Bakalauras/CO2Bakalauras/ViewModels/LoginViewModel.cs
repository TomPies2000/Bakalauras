using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CO2Bakalauras.Services;
using CO2Bakalauras.Models;
using Newtonsoft.Json.Linq;

namespace CO2Bakalauras.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; set; }
        public string Login { get; set; }
        public string Psw { get; set; }

        private bool _activityIndicator;
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

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked()
        {
            ActivityIndicator = true;
            WebService webService = new WebService();
            Vartotojas vartotojas = await webService.GetUserByLogin(Login, Psw);
            

            if (vartotojas == null)
            {
                await Application.Current.MainPage.DisplayAlert("Prisijungimas", "Prisijungimas nesėkmingas", "Pakartoti");
                ActivityIndicator = false;
                return;
            }
            else
            {
                Administratorius admin = await webService.GetAdminByUserID(vartotojas.VARTOTOJO_ID);
                if (admin != null)
                    ((App)App.Current).CurrentAdmin = admin;
                ((App)App.Current).CurrentUser = vartotojas;
                List<Sanaudos> sanaudos = await webService.GetUserUsage(vartotojas.VARTOTOJO_ID);
                if (sanaudos == null || sanaudos.Count == 0)
                {
                    await Shell.Current.GoToAsync($"//{nameof(MileagePage)}");
                }
                else
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

            }
            ActivityIndicator = false;
        }
    }
}
