using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CO2Bakalauras.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPageFlyout : ContentPage
    {
        public ListView ListView;

        public MenuPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MenuPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MenuPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuPageFlyoutMenuItem> MenuItems { get; set; }
            
            public MenuPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MenuPageFlyoutMenuItem>(new[]
                {
                    new MenuPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new MenuPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new MenuPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new MenuPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new MenuPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}