using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        WebService web = new WebService();
        readonly Vartotojas vartotojas = ((App)App.Current).CurrentUser;
        public Command UpdateProfileCommand { get; set; }
        public Command UpdatePasswordCommand { get; set; }
        public Command SignOutCommand { get; set; }
        public Command DeleteProfileCommand { get; set; }
        public ProfileViewModel()
        {
            UpdateProfileCommand = new Command(UpdateProfile);
            UpdatePasswordCommand = new Command(UpdatePassword);
            SignOutCommand = new Command(SignOut);
            DeleteProfileCommand = new Command(DeleteProfile);
        }

        async void SignOut()
        {
            ((App)App.Current).CurrentUser = null;
            await Shell.Current.GoToAsync($"//{nameof(FirstPage)}");
            await Shell.Current.Navigation.PopToRootAsync();
        }

        async void UpdateProfile()
        {
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}/{nameof(ChangeProfilePage)}");
        }
        async void UpdatePassword()
        {
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}/{nameof(ChangePasswordPage)}");
        }

        async void DeleteProfile()
        {
            bool pasalinimoPatvirtinimas = await Shell.Current.DisplayAlert("Įspėjimas", "Ar tikrai norite pašalinti profilį?", "Taip", "Ne");

            if (pasalinimoPatvirtinimas)
            {
                List<Sanaudos> sanaudosList = await web.GetUserUsage(vartotojas.VARTOTOJO_ID);
                List<Statistika> statistikaList = await web.GetStatisticByUserId(vartotojas.VARTOTOJO_ID);
                List<Atlieka> atliekaList = await web.GetExecutedTasksByUserID(vartotojas.VARTOTOJO_ID);
                List<Draugauja> draugaujaList = await web.GetFriendByID(vartotojas.VARTOTOJO_ID);

                foreach (Sanaudos u in sanaudosList)
                {
                    await web.DeleteHouseByUsageID(u.SANAUDU_ID);
                    await web.DeleteCarByUsageID(u.SANAUDU_ID);
                }
                await web.DeleteUsageByUserID(vartotojas.VARTOTOJO_ID);
                await web.DeleteExecutedTasksByUserID(vartotojas.VARTOTOJO_ID);
                await web.DeleteStatisticByUserID(vartotojas.VARTOTOJO_ID);
                await web.DeleteAllUserFriendships(vartotojas.VARTOTOJO_ID);
                await web.DeleteUser(vartotojas.VARTOTOJO_ID);

                await Shell.Current.GoToAsync($"//{nameof(FirstPage)}");
            }
        }
    }
}
