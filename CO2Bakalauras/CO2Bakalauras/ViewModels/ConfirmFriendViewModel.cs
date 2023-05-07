using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;


namespace CO2Bakalauras.ViewModels
{
    public class ConfirmFriendViewModel : BindableObject
    {
        WebService web = new WebService();
        readonly Vartotojas CurrentUser = ((App)App.Current).CurrentUser;
        private ObservableCollection<Vartotojas> friendsList;
        public Command ConfirmFriendCommand { get; set; }
        public Command DeclineFriendCommand { get; set; }
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

        Vartotojas selectedFriend;
        public Vartotojas SelectedFriend
        {
            get
            {
                return selectedFriend;
            }
            set
            {
                selectedFriend = value;
                OnPropertyChanged();
            }

        }
        public ConfirmFriendViewModel()
        {
            FriendsList = new ObservableCollection<Vartotojas>();
            ConfirmFriendCommand = new Command(ConfirmFriend);
            DeclineFriendCommand = new Command(DeclineFriend);
        }

        async void Load()
        {
            FriendsList.Clear();
            List<Draugauja> draugauja;
            draugauja = await web.GetFriendRequestByID(CurrentUser.VARTOTOJO_ID);

            foreach (Draugauja u in draugauja)
            {
                if (u.PATVIRTINTAS == false)
                {
                    Vartotojas vartotojas = await web.GetUserByID(u.PIRMO_DRAUGO_ID);
                    FriendsList.Add(vartotojas);
                }
            }
        }

        async void ConfirmFriend()
        {
            if(SelectedFriend != null) 
            {
                Draugauja patvirtinimas = new Draugauja
                {
                    PIRMO_DRAUGO_ID = CurrentUser.VARTOTOJO_ID,
                    ANTRO_DRAUGO_ID = SelectedFriend.VARTOTOJO_ID,
                    PATVIRTINTAS = true
                };

                await web.AddFriend(patvirtinimas);
                Draugauja draugauja = await web.GetNewFriendByID(SelectedFriend.VARTOTOJO_ID, CurrentUser.VARTOTOJO_ID);
                draugauja.PATVIRTINTAS = true;

                await web.UpdateDraugauja(draugauja);
                await Application.Current.MainPage.DisplayAlert("Pranešimas", "Draugas pridėtas", "Ok");
                await Shell.Current.GoToAsync($"//{nameof(FriendsPage)}");
                Load();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Pranešimas", "Pasirinkite draugą, kurį norite pridėti", "Ok");
            }
        }

        async void DeclineFriend()
        {

            if (SelectedFriend != null)
            {
                Draugauja atmetimas = new Draugauja
                {
                    PIRMO_DRAUGO_ID = SelectedFriend.VARTOTOJO_ID,
                    ANTRO_DRAUGO_ID = CurrentUser.VARTOTOJO_ID,
                    PATVIRTINTAS = false
                };

                await web.DeclineRequest(atmetimas);
                await Application.Current.MainPage.DisplayAlert("Pranešimas", "Draugas atmestas", "Ok");
                await Shell.Current.GoToAsync($"//{nameof(FriendsPage)}");
                Load();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Pranešimas", "Pasirinkite draugą, kurį norite pridėti", "Ok");
            }

        }

        public void VModelActive()
        {
            Load();
        }
    }
}
