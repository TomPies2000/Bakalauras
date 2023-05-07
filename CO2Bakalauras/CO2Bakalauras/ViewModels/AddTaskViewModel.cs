using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    [QueryProperty(nameof(TaskID), "taskID")]
    public class AddTaskViewModel : BaseViewModel
    {
        readonly Administratorius administratorius = ((App)App.Current).CurrentAdmin;
        WebService webService = new WebService();
        private string name;
        private string description;
        private string points;
        private string selected;
        private TimeSpan selectedTime;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
        public string Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
                OnPropertyChanged();
            }
        }
        public string Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan SelectedTime
        {
            get
            {
                return selectedTime;
            }
            set
            {
                selectedTime = value;
                OnPropertyChanged();
            }
        }
        public Command AddTaskCommand { get; set; }
        public DateTime date = DateTime.Now.Date;
        private int taskId; 
        private string buttonText;
        public int TaskID
        {
            get
            {
                return taskId;
            }
            set
            {
                taskId = value;
                Load();
            }
        }
        public List<string> Categorie { get; set; }
        public string ButtonText
        {
            get
            {
                return buttonText;
            }
            set
            {
                buttonText = value;
                OnPropertyChanged();
            }
        }

        public AddTaskViewModel()
        {
            AddTaskCommand = new Command(AddTask);

            Categorie = new List<string>
            {
            "Elektra",
            "Šildymas",
            "Vanduo",
            "Transportas"
            };
        }

        async void AddTask()
        {
            
            Uzduotis uzduotis = new Uzduotis();
            if(Name == null || Name.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite užduoties pavadinimą", "Pakartoti");
                return;
            }
            else if (Description == null || Description.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite užduoties aprašymą", "Pakartoti");
                return;
            }
            else if (Points == null || Points.Length == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Įrašykite užduoties taškų skaičių", "Pakartoti");
                return;
            }
            else if (SelectedTime == null)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Pasirinkite užduoties priminimo laiką", "Pakartoti");
                return;
            }
            else if (Selected == null)
            {
                await Application.Current.MainPage.DisplayAlert("Oops..", "Pasirinkite užduoties kategoriją", "Pakartoti");
                return;
            }
            else
            {
                uzduotis.PAVADINIMAS = Name;
                uzduotis.TASKU_SKAICIUS = byte.Parse(Points);
                uzduotis.ADMINISTRATORIAUS_ID = administratorius.ADMINISTRATORIAUS_ID;
                uzduotis.KATEGORIJA = Selected;
                uzduotis.APRASYMAS = Description;
                uzduotis.PRIMINIMO_LAIKAS = date.Add(SelectedTime);
                uzduotis.PRIMINIMO_LAIKAS = DateTime.SpecifyKind(uzduotis.PRIMINIMO_LAIKAS, DateTimeKind.Utc);

                if (TaskID > 0)
                {
                    uzduotis.UZDUOTIES_ID = TaskID;
                    await webService.UpdateTask(uzduotis);
                    await Application.Current.MainPage.DisplayAlert("Pranešimas", "Užduotis atnaujinta", "Gerai");
                    await Shell.Current.GoToAsync($"//{nameof(TaskPage)}");
                }
                else
                {
                    await webService.CreateTask(uzduotis);
                    await Application.Current.MainPage.DisplayAlert("Pranešimas", "Užduotis sukurta", "Gerai");
                    await Shell.Current.GoToAsync($"//{nameof(TaskPage)}");
                }
                

                
            }
        }

        async void Load()
        {

            if(TaskID > 0)
            {
                ButtonText = "Atnaujinti užduotį";
                Uzduotis uzd = await webService.GetTaskByID(TaskID);
                Name = uzd.PAVADINIMAS.ToString();
                Description = uzd.APRASYMAS.ToString();
                Points = uzd.TASKU_SKAICIUS.ToString();
                Selected = uzd.KATEGORIJA.ToString();
                SelectedTime = SelectedTime.Add(uzd.PRIMINIMO_LAIKAS.TimeOfDay);
            }
            else
            {
                ButtonText = "Pridėti užduotį";
            }


        }

    }
}
