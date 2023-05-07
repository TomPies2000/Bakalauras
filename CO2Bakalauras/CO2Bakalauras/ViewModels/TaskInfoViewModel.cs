using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    [QueryProperty(nameof(TaskID), "taskID")]
    public class TaskInfoViewModel : BindableObject
    {
        WebService webService = new WebService();
        public Vartotojas vartotojas = ((App)App.Current).CurrentUser;
        public Administratorius admin = ((App)App.Current).CurrentAdmin;
        public Uzduotis uzduotis = new Uzduotis();
        private int taskId;
        private string name;
        private string description;
        private string points;
        private string category;
        private bool isAdmin;
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
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                OnPropertyChanged();
            }
        }
        public Command DoTaskCommand { get; set; }
        public Command DeleteTaskCommand { get; set; }
        public Command UpdateTaskCommand { get; set; }

        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
                OnPropertyChanged();
            }
        }
        public TaskInfoViewModel()
        {
            DoTaskCommand = new Command(DoTask);
            DeleteTaskCommand = new Command(DeleteTask);
            UpdateTaskCommand = new Command(UpdateTask);
        }
        async void Load() 
        {
            uzduotis = await webService.GetTaskByID(taskId);
            if (uzduotis == null)
            {
                return;
            }
            Name = uzduotis.PAVADINIMAS;
            Description = "Aprašymas: " + uzduotis.APRASYMAS;
            Points = "Taškų skaičius: " + uzduotis.TASKU_SKAICIUS.ToString();
            Category = uzduotis.KATEGORIJA;

            if(admin == null)
            {
                IsAdmin = false;
            }
            else 
            {
                IsAdmin = true;
            }
        }
        async void DoTask()
        {

            Atlieka atlieka = new Atlieka {
                VARTOTOJO_ID = vartotojas.VARTOTOJO_ID,
                UZDUOTIES_ID = uzduotis.UZDUOTIES_ID
            };
            List<Statistika> statistikaList = await webService.GetStatisticByUserId(vartotojas.VARTOTOJO_ID);
            Statistika statistika = statistikaList.OrderByDescending(o => o.LAIKOTARPIS).Take(1).FirstOrDefault();
            await webService.ExecuteTask(atlieka);
            await PointsLogic.ManagePoints(statistika, uzduotis.TASKU_SKAICIUS);
            await Application.Current.MainPage.DisplayAlert("Super!", "Nepamirškite atlikti užduoties :)", "Gerai");
            await Shell.Current.GoToAsync($"//{nameof(TaskPage)}");
        }

        async void DeleteTask()
        {
            await webService.DeleteExecutedTasksByTaskID(uzduotis.UZDUOTIES_ID);
            await webService.DeleteTaskByID(uzduotis.UZDUOTIES_ID);
            await Application.Current.MainPage.DisplayAlert("Super!", "Užduotis pašalinta", "Gerai");
            await Shell.Current.GoToAsync($"//{nameof(TaskPage)}");
        }

        async void UpdateTask()
        {
            await Shell.Current.GoToAsync($"/{nameof(AddTaskPage)}?taskID={uzduotis.UZDUOTIES_ID}");
        }
    }
}
