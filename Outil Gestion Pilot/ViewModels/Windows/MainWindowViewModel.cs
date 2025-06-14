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
            },
            new NavigationViewItem()
            {
                Content = "Revendeurs",
                Icon = new SymbolIcon { Symbol = SymbolRegular.PersonSquare20 },
                TargetPageType = typeof(Views.Pages.DealersPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Paramètres",
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
