using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CO2Bakalauras.Services;
using CO2Bakalauras.Models;
using System.Linq;

namespace CO2Bakalauras.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public string Login { get; set; }
        public string Psw { get; set; }


        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            WebService webService = new WebService();
            Vartotojas vartotojas = await webService.GetUserByLogin(Login,Psw);
            if(vartotojas == null) 
            {
                await Application.Current.MainPage.DisplayAlert("Prisijungimas", "Prisijungimas nesekmingas", "Pakartoti");
            }
            else
                await Application.Current.MainPage.DisplayAlert("Prisijungimas", "Prisijungimas sekmingas", "Gerai");
            //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
