using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class RegisterViewModel:BindableObject
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Psw1 { get; set; }
        public string Psw2 { get; set; }
        public Command CreateUser { get; set; }

        public RegisterViewModel()
        {
            CreateUser = new Command(CreateNewUser);
        }
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
        private async void CreateNewUser()
        {

            WebService webService = new WebService();
            Vartotojas vartotojas = new Vartotojas();
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            Vartotojas login = await webService.GetUserByName(Login);

            if (Login == null || Login.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite prisijungimo vardą", "Pakartoti");
                return;
            }
            else if (login != null)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Vartotojas su tokiu prisijungimo vardu jau egzistuoja", "Pakartoti");
                return;
            }
            else if (Name == null || Name.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite vardą", "Pakartoti");
                return;
            }
            else if (Surname == null || Surname.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite pavardę", "Pakartoti");
                return;
            }
            else if (Email == null || Email.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite elektroninį paštą", "Pakartoti");
                return;
            }
            else if (Psw1 == null || Psw2 == null || Psw1.Length == 0 || Psw2.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite slaptažodžius", "Pakartoti");
                return;
            }
            else if(Psw1 != Psw2)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Nesutampa slaptažodžiai", "Pakartoti");
                return;
            }
            else if (Psw1.Length < 7)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Slaptažodis turi būti netrumpesnis kaip iš 8 ženklų", "Pakartoti");
                return;
            }
            else if (!Psw1.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Slaptažodis turi turėti skaičių, specialujį ženklą, mažą ir didelę raidę", "Pakartoti");
                return;
            }
            else
            {
                ActivityIndicator = true;
                vartotojas.PRISIJUNGIMO_VARDAS = Login;
                vartotojas.VARTOTOJO_VARDAS = Name;
                vartotojas.VARTOTOJO_PAVARDE = Surname;
                vartotojas.VARTOTOJO_EMAIL = Email;
                vartotojas.VARTOTOJO_SLAPTAZODIS = Psw1;
                await webService.CreateUser(vartotojas);
                vartotojas = await webService.GetUserByName(Login);
                Statistika statistika = new Statistika
                {
                    VARTOTOJO_ID =vartotojas.VARTOTOJO_ID,
                    LYGIS = 1,
                    LYGIO_PAVADINIMAS = "Naujokas",
                    TASKU_SUMA = 0,
                    LAIKOTARPIS = DateTime.Now
                };
                await webService.CreateStatistic(statistika);
                await Application.Current.MainPage.DisplayAlert("Registracija", "Registracija sėkminga", "Gerai");
                await Shell.Current.GoToAsync($"//{nameof(FirstPage)}");
            }
            ActivityIndicator = false;

        }
    }
}
