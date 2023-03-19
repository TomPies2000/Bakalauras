using CO2Bakalauras.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CO2Bakalauras.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}