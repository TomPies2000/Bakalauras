using CO2Bakalauras.ViewModels;
using CO2Bakalauras.Views;
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
    public partial class StatisticPage : ContentPage
    {
        readonly StatisticViewModel viewModelClass;
        public StatisticPage()
        {
            InitializeComponent();
            viewModelClass = new StatisticViewModel();
            this.BindingContext = viewModelClass;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModelClass.VModelActive();
        }
    }
}