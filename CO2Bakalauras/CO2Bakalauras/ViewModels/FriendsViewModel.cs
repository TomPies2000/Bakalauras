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
    class FriendsViewModel : BindableObject
    {
        WebService web = new WebService();
        readonly Vartotojas CurrentUser = ((App)App.Current).CurrentUser;
        readonly Administratorius admin = ((App)App.Current).CurrentAdmin;

        private ObservableCollection<Draugas> friendsList;
        private ObservableCollection<Draugas> friendsListCopy;

        public Command AddFriendCommand { get; set; }
        public Command ConfirmFriendCommand { get; set; }


        Draugas selectedFriend;
        public Draugas SelectedFriend
        {
            get
            {
                return selectedFriend;
            }
            set
            {
                selectedFriend = value;
                OpenFriendInfo(selectedFriend.Vartotojas);
                selectedFriend = null;
                OnPropertyChanged();
            }

        }

        public ObservableCollection<Draugas> FriendsList
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
        public ObservableCollection<Draugas> FriendsListCopy
        {
            get
            {
                return friendsListCopy;
            }
            set
            {
                friendsListCopy = value;
                OnPropertyChanged();
            }
        }

        public FriendsViewModel()
        {
            FriendsList = new ObservableCollection<Draugas>();
            FriendsListCopy = new ObservableCollection<Draugas>();

            AddFriendCommand = new Command(AddFriend);
            ConfirmFriendCommand = new Command(ConfirmFriend);
        }

        async void ConfirmFriend()
        {
            await Shell.Current.GoToAsync($"//{nameof(FriendsPage)}/{nameof(ConfirmFriendPage)}");
        }

        async void AddFriend()
        {
            await Shell.Current.GoToAsync($"//{nameof(FriendsPage)}/{nameof(AddFriendPage)}");
        }

        async void Load()
        {
            friendsList.Clear();
            
            List<Vartotojas> vartotojai = new List<Vartotojas>();
            List<Draugauja> draugauja;
            List<Statistika> statistikaList;
            Statistika statistika;
            Vartotojas vartotojas;
            draugauja = await web.GetFriendByID(CurrentUser.VARTOTOJO_ID);
            foreach(Draugauja u in draugauja)
            {
                vartotojas = await web.GetUserByID(u.ANTRO_DRAUGO_ID);
                statistikaList = await web.GetStatisticByUserId(u.ANTRO_DRAUGO_ID);
                statistika = statistikaList.OrderByDescending(o => o.LAIKOTARPIS).Take(1).FirstOrDefault();
                if(u.PATVIRTINTAS == true)
                    friendsList.Add(new Draugas(vartotojas, statistika));
            }
        }


        async void OpenFriendInfo(Vartotojas vartotojas)
        {
            await Shell.Current.GoToAsync($"//{nameof(FriendsPage)}/{nameof(FriendInfoPage)}?userID={vartotojas.VARTOTOJO_ID}");
        }


        public void VModelActive()
        {
            Load();
        }
    }
}
