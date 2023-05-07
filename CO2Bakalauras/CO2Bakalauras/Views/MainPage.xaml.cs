using CO2Bakalauras.ViewModels;
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
    public partial class MainPage : ContentPage
    {
        readonly MainViewModel viewModelClass;
        public MainPage()
        {
            InitializeComponent();
            viewModelClass = new MainViewModel();
            this.BindingContext = viewModelClass;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModelClass.VModelActive();
        }

    }
}