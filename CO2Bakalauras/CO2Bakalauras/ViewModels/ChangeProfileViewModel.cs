using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class ChangeProfileVieWModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Command UpdateProfileCommand { get; set; }
        readonly Vartotojas vartotojas = ((App)App.Current).CurrentUser;

        public ChangeProfileVieWModel()
        {
            Load();
            UpdateProfileCommand = new Command(UpdateProfile);
        }

        async void UpdateProfile()
        {
            WebService webService = new WebService();
            vartotojas.PRISIJUNGIMO_VARDAS = Login;
            vartotojas.VARTOTOJO_VARDAS = Name;
            vartotojas.VARTOTOJO_PAVARDE = Surname;
            vartotojas.VARTOTOJO_EMAIL = Email;
            await webService.UpdateUserProfile(vartotojas);
            ((App)App.Current).CurrentUser = vartotojas;
            await Application.Current.MainPage.DisplayAlert("Super :)", "Pakeitimai sekmingai išsaugoti", "Ok");
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
        }
        void Load()
        {
            Login = vartotojas.PRISIJUNGIMO_VARDAS;
            Name = vartotojas.VARTOTOJO_VARDAS;
            Surname = vartotojas.VARTOTOJO_PAVARDE;
            Email = vartotojas.VARTOTOJO_EMAIL;
        }
    }
}
