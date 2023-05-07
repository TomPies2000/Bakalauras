using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using CO2Bakalauras.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class TaskViewModel : BindableObject
    {
        readonly Administratorius admin = ((App)App.Current).CurrentAdmin;
        public Command AddTaskCommand { get; set; }
        private ObservableCollection<Uzduotis> taskList;
        private ObservableCollection<Uzduotis> taskListCopy;
        Uzduotis uzduotis;
        string selected;
        public Uzduotis SelectedTask
        {
            get
            {
                return uzduotis;
            }
            set
            {
                uzduotis = value;
                OpenTask(uzduotis);
                uzduotis = null;
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
                FilterList();
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Uzduotis> TaskList
        {
            get 
            { 
                return taskList; 
            }
            set 
            { 
                taskList = value; 
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Uzduotis> TaskListCopy
        {
            get
            {
                return taskListCopy;
            }
            set
            {
                taskListCopy = value;
                OnPropertyChanged();
            }
        }

        public TaskViewModel()
        {
            TaskList = new ObservableCollection<Uzduotis>();
            TaskListCopy = new ObservableCollection<Uzduotis>();
            AddTaskCommand = new Command(AddTask);
        }
        void FilterList()
        {
            if (selected == null)
                return;
            switch (Selected)
            {
                case "Elektrą" :
                    ResetList();
                    TaskList = new ObservableCollection<Uzduotis>(TaskList.Where(x => x.KATEGORIJA == "Elektra").ToList());
                    break;
                case "Šildymą":
                    ResetList();
                    TaskList = new ObservableCollection<Uzduotis>(TaskList.Where(x => x.KATEGORIJA == "Šildymas").ToList());
                    break;
                case "Transportą":
                    ResetList();
                    TaskList = new ObservableCollection<Uzduotis>(TaskList.Where(x => x.KATEGORIJA == "Transportas").ToList());
                    break;
                case "Vandenį":
                    ResetList();
                    TaskList = new ObservableCollection<Uzduotis>(TaskList.Where(x => x.KATEGORIJA == "Vanduo").ToList());
                    break;
                case "Visas":
                    ResetList();
                    break;
                default:
                    return;

            }
        }

        private void ResetList()
        {
            TaskList.Clear();
            foreach (Uzduotis u in TaskListCopy)
            {
                TaskList.Add(u);
            }
        }

        async void Load()
        {
            TaskList.Clear();
            TaskListCopy.Clear();
            WebService web = new WebService();
            List<Uzduotis> task;
            task = await web.GetTask();
            foreach (Uzduotis u in task)
            {
                TaskList.Add(u);
            }
            foreach (Uzduotis u in TaskList)
            {
                TaskListCopy.Add(u);
            }

        }

        async void OpenTask(Uzduotis uzduotis)
        {
            await Shell.Current.GoToAsync($"//{nameof(TaskPage)}/{nameof(TaskInfoPage)}?taskID={uzduotis.UZDUOTIES_ID}");
        }

        public void VModelActive()
        {
            Load();
        }

        async void AddTask()
        {
            if (admin != null)
            {
                await Shell.Current.GoToAsync($"//{nameof(TaskPage)}/{nameof(AddTaskPage)}?taskID={0}");
            }

        }

    }
}
