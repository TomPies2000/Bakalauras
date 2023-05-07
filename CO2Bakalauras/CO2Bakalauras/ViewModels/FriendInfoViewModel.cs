using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    [QueryProperty(nameof(UserID), "userID")]
    public class FriendInfoViewModel : BindableObject
    {

        WebService web = new WebService();
        Vartotojas vartotojas = new Vartotojas();
        List<ChartEntry> entries = new List<ChartEntry>();
        private List<Statistika> statistikaList;
        readonly Vartotojas CurrentUser = ((App)App.Current).CurrentUser;

        int userID;
        private string vardas;
        private string pavarde;
        private string lygis;
        private string taskuSkaicius;
        private Chart chart;
        private bool deleteButtonVisible;
        public Command DeleteFriendCommand { get; set; }
        public bool DeleteButtonVisible
        {
            get
            {
                return deleteButtonVisible;
            }
            set
            {
                deleteButtonVisible = value;
                OnPropertyChanged();
            }
        }
        public string Vardas
        {
            get
            {
                return vardas;
            }
            set
            {
                vardas = value;
                OnPropertyChanged();
            }
        }
        public string Pavarde
        {
            get
            {
                return pavarde;
            }
            set
            {
                pavarde = value;
                OnPropertyChanged();

            }
        }
        public string Lygis
        {
            get
            {
                return lygis;
            }
            set
            {
                lygis = value;
                OnPropertyChanged();
            }
        }
        public string TaskuSkaicius
        {
            get
            {
                return taskuSkaicius;
            }
            set
            {
                taskuSkaicius = value;
                OnPropertyChanged();
            }
        }
        public Chart ChartView
        {
            get
            {
                return chart;
            }
            set
            {
                chart = value;
                OnPropertyChanged();
            }
        }

        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
                Load();
            }
        }

        public FriendInfoViewModel()
        {
            statistikaList = new List<Statistika>();
            DeleteFriendCommand = new Command(DeleteFriend);
        }

        async void Load()
        {
            vartotojas = await web.GetUserByID(userID);
            statistikaList = await web.GetStatisticByUserId(userID);
            Statistika statistika = statistikaList.OrderByDescending(o => o.LAIKOTARPIS).Take(1).FirstOrDefault();
            Lygis = "Esamas lygio pavadinimas: " + statistika.LYGIO_PAVADINIMAS;
            TaskuSkaicius = "Esamas taškų skaičius " + statistika.TASKU_SUMA;
            foreach (Statistika u in statistikaList)
            {
                entries.Add(new ChartEntry((float)u.LYGIS)
                {
                    Label ="Mėnesis " + u.LAIKOTARPIS.Month.ToString(),
                    ValueLabel = u.LYGIS.ToString(),
                });
            }
            ChartView = new LineChart { Entries = entries, LabelTextSize = 50, BackgroundColor = SKColors.Transparent };

            if(CurrentUser.VARTOTOJO_ID == UserID)
            {
                DeleteButtonVisible = false; 
            }
            else 
            {
                DeleteButtonVisible = true;
            }


        }

        async void DeleteFriend()
        {

            Draugauja draugauja = await web.GetNewFriendByID(CurrentUser.VARTOTOJO_ID, vartotojas.VARTOTOJO_ID);
            await web.DeleteFriend(draugauja);
            await Application.Current.MainPage.DisplayAlert("Pranešimas", "Draugas pašalintas", "Gerai");
            Load();

        }

    }
}
