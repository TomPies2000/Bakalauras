using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class FirstViewModel : BindableObject
    {

        public Command LoginWithGoogle { get; set; }
        public Command Login { get; set; }
        public Command Register { get; set; }
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

        public FirstViewModel()
        {

            LoginWithGoogle = new Command(LoginGoogle);
            Login = new Command(LoginApp);
            Register = new Command(Registration);
        }

        private void LoginGoogle()
        {
            ActivityIndicator = true;

            ActivityIndicator = false;
        }

        private async void LoginApp()
        {
            ActivityIndicator = true;
            await Shell.Current.GoToAsync("//FirstPage/LoginPage");
            ActivityIndicator = false;
        }

        private async void Registration()
        {
            ActivityIndicator = true;
            await Shell.Current.GoToAsync("//FirstPage/RegisterPage");
            ActivityIndicator = false;
        }

    }
}
