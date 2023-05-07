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
	public partial class FriendsPage : ContentPage
	{
        readonly FriendsViewModel viewModelClass;
        public FriendsPage ()
		{
			InitializeComponent ();
            viewModelClass = new FriendsViewModel();
            this.BindingContext = viewModelClass;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModelClass.VModelActive();
        }
    }
}