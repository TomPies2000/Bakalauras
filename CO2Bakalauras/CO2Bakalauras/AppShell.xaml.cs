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
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(FirstPage), typeof(FirstPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }

    }
}
