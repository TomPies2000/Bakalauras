using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class AddFriendViewModel : BindableObject
    {
        public string Text { get; set; }
        public Command FindFriendCommand { get; set; }
        public Command AddFriendCommand { get; set; }
        readonly Vartotojas CurrentUser = ((App)App.Current).CurrentUser;
        public Vartotojas vartotojas = new Vartotojas();
        WebService web = new WebService();
        public AddFriendViewModel()
        {
            FriendsList = new ObservableCollection<Vartotojas>();
            FindFriendCommand = new Command(FindFriend);
            AddFriendCommand = new Command(AddFriend);
        }

        private ObservableCollection<Vartotojas> friendsList;
        public ObservableCollection<Vartotojas> FriendsList
        {
            get
            {
                return friendsList;
            }
            set
            {
                friendsList = value;
                OnPropertyChanged();
            }
        }

        async void FindFriend()
        {
            if (FriendsList.Count > 0) 
            {
                FriendsList.Clear();
            }
            vartotojas = await web.GetUserByName(Text);
            if (vartotojas != null)
            {
                Draugauja draugauja = await web.GetNewFriendByID(CurrentUser.VARTOTOJO_ID, vartotojas.VARTOTOJO_ID);
                if (draugauja == null && Text != CurrentUser.PRISIJUNGIMO_VARDAS)
                    FriendsList.Add(vartotojas);
                else
                    await Application.Current.MainPage.DisplayAlert("Pranešimas", "Vartotojas jau pridėtas prie draugų", "Ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Pranešimas", "Tokio vartotojo nerasta", "Ok");
            }
        }

        async void AddFriend()
        {
            if(vartotojas != null)
            {
                Draugauja draugauja = new Draugauja
                {
                    PIRMO_DRAUGO_ID = CurrentUser.VARTOTOJO_ID,
                    ANTRO_DRAUGO_ID = vartotojas.VARTOTOJO_ID,
                    PATVIRTINTAS = false
                };
                await web.AddFriend(draugauja);
                await Application.Current.MainPage.DisplayAlert("Pranešimas", "Draugas sekmingai pridėtas", "Ok");
                await Shell.Current.GoToAsync($"//{nameof(FriendsPage)}");
            }
        }
    }
}
