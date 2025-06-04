using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Pilot";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = "Data",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            },
            new NavigationViewItem()
            {
                Content = "Produits",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Box24 },
                TargetPageType = typeof(Views.Pages.ProductsPage)
            },
            new NavigationViewItem()
            {
                Content = "Panier",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Cart24 },
                TargetPageType = typeof(Views.Pages.CartPage)
            },
            new NavigationViewItem()
            {
                Content = "Commandes",
                Icon = new SymbolIcon { Symbol = SymbolRegular.NotepadEdit20 },
                TargetPageType = typeof(Views.Pages.OrdersPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
