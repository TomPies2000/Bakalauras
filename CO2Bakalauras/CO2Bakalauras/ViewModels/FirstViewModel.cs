using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class FirstViewModel: BindableObject
    {
        public Command LoginWithGoogle { get; set; }
        public Command Login { get; set; }
        public Command Register { get; set; }

        public FirstViewModel()
        {
            LoginWithGoogle = new Command(LoginGoogle);
            Login = new Command(LoginApp);
            Register = new Command(Registration);
        }

        private void LoginGoogle()
        {

        }

        private async void LoginApp()
        {
            await Shell.Current.GoToAsync("//FirstPage/LoginPage");
        }

        private void Registration() 
        {
        
        }

    }
}
