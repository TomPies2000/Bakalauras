using CO2Bakalauras.ViewModels;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CO2Bakalauras
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FirstPage), typeof(FirstPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(MileagePage), typeof(MileagePage));
            Routing.RegisterRoute(nameof(HousePage), typeof(HousePage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
