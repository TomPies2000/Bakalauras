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
            Routing.RegisterRoute(nameof(AddUsagePage), typeof(AddUsagePage));
            Routing.RegisterRoute(nameof(AddUsagePage2), typeof(AddUsagePage2));
            Routing.RegisterRoute(nameof(CheckUsagePage), typeof(CheckUsagePage));

            Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
            Routing.RegisterRoute(nameof(ChangeProfilePage), typeof(ChangeProfilePage));
            Routing.RegisterRoute(nameof(TaskInfoPage), typeof(TaskInfoPage));
            Routing.RegisterRoute(nameof(AddTaskPage), typeof(AddTaskPage));


            Routing.RegisterRoute(nameof(AddFriendPage), typeof(AddFriendPage));
            Routing.RegisterRoute(nameof(ConfirmFriendPage), typeof(ConfirmFriendPage));
            Routing.RegisterRoute(nameof(FriendInfoPage), typeof(FriendInfoPage));


        }
    }
}
