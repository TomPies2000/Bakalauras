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
    public class ChangePasswordViewModel
    {
        Vartotojas vartotojas = ((App)App.Current).CurrentUser;
        public string OldPsw { get; set; }
        public string NewPsw1 { get; set; }
        public string NewPsw2 { get; set; }
        public Command UpdatePasswordCommand { get; set; }
        public ChangePasswordViewModel()
        {
            UpdatePasswordCommand = new Command(UpdatePassword);
        }

        async void UpdatePassword()
        {
            if ((OldPsw == null || OldPsw.Length == 0) && (NewPsw1 == null || NewPsw1.Length == 0) && (NewPsw2 == null || NewPsw2.Length == 0))
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite slaptažodžius ", "Pakartoti");
                return;
            }
            else if (NewPsw1 != NewPsw2)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Nesutampa nauji slaptažodžiai", "Pakartoti");
                return;
            }
            else if (NewPsw1.Length < 7)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Slaptažodis turi būti netrumpesnis kaip iš 8 ženklų", "Pakartoti");
                return;
            }
            else if (!NewPsw1.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Slaptažodis turi turėti skaičių, specialujį ženklą, mažą ir didelę raidę", "Pakartoti");
                return;
            }
            else
            {
                WebService webService = new WebService();
                vartotojas.VARTOTOJO_SLAPTAZODIS = NewPsw1;
                await webService.UpdateUserProfile(vartotojas);
                ((App)App.Current).CurrentUser = vartotojas;
                await Application.Current.MainPage.DisplayAlert("Super :)", "Slaptažodis sekmingai išsaugotas", "Ok");
                await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
            }
                

        }
    }
}
