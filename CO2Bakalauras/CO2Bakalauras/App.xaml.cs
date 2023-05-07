using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CO2Bakalauras
{
    public partial class App : Application
    {
        public Vartotojas CurrentUser { get; set; }
        public Administratorius CurrentAdmin { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
