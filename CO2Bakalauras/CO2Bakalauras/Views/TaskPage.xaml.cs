﻿using CO2Bakalauras.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CO2Bakalauras.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskPage : ContentPage
    {
        readonly TaskViewModel viewModelClass;
        public TaskPage()
        {
            InitializeComponent();
            viewModelClass = new TaskViewModel();
            this.BindingContext = viewModelClass;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModelClass.VModelActive();
        }
    }
}