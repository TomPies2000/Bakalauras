using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class StatisticViewModel : BindableObject
    {
        public string lygis;
        public string taskai;
        public string ikiLygio;
        public string uzduotis;

        public string Uzduotis
        {
            get
            {
                return uzduotis;
            }
            set
            {
                uzduotis = value;
                OnPropertyChanged();
            }
        }
        public string Lygis {
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
        public string Taskai
        {
            get
            {
                return taskai;
            }
            set
            {
                taskai = value;
                OnPropertyChanged();
            }
        }
        public string IkiLygio
        {
            get
            {
                return ikiLygio;
            }
            set
            {
                ikiLygio = value;
                OnPropertyChanged();
            }
        }
        readonly Vartotojas vartotojas = ((App)App.Current).CurrentUser;

        public Command OpenStatisticInfoCommand { get; set; }
        public StatisticViewModel()
        {
            OpenStatisticInfoCommand = new Command(OpenStatisticInfo);
        }

        async void Load()
        {
            WebService webService = new WebService();
            List<Statistika> statistikaList = new List<Statistika>();
            statistikaList = await webService.GetStatisticByUserId(vartotojas.VARTOTOJO_ID);
            Statistika statistika = statistikaList.OrderByDescending(o => o.LAIKOTARPIS).Take(1).FirstOrDefault();

            Lygis = "Lygis: " + Convert.ToString(statistika.LYGIS);
            Taskai = "Taškų suma: " + Convert.ToString(statistika.TASKU_SUMA);

            List<Uzduotis> taskList = await webService.GetTask();

            int taskCount = taskList.Count();

            Random rnd = new Random();

            Uzduotis = taskList[rnd.Next(0, taskCount - 1)].APRASYMAS;

            short iki = PointsLogic.ToNextLevel(statistika);
            IkiLygio = "Iki sekančio lygio liko: " + (iki-statistika.TASKU_SUMA).ToString();
        }

        public void VModelActive()
        {
            Load();
        }
        
        async void OpenStatisticInfo()
        {
            await Shell.Current.GoToAsync($"//{nameof(StatisticPage)}/{nameof(FriendInfoPage)}?userID={vartotojas.VARTOTOJO_ID}");
        }

    }
}
