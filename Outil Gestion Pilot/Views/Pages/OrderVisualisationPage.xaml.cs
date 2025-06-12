using Npgsql;
using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui;
using Wpf.Ui.Abstractions.Controls;

namespace Outil_Gestion_Pilot.Views.Pages
{
    /// <summary>
    /// Logique d'interaction pour OrderVisualisationPage.xaml
    /// </summary>
    public partial class OrderVisualisationPage : Page
    {
        public static Order laCommande;
        INavigationService navigationService;

        private OrderVisualisationViewModel ViewModel { get; }


        public OrderVisualisationPage(Order uneCommande, OrderVisualisationViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            SetPriceTTC();
            this.DataContext = new ViewModels.Pages.OrderVisualisationViewModel(uneCommande);

        }

        public OrderVisualisationPage(Order uneCommande)
        {
            laCommande = uneCommande;
            ViewModel = new OrderVisualisationViewModel(laCommande); // Initialisation de ViewModel
            InitializeComponent();
            SetPriceTTC();
            this.DataContext = new ViewModels.Pages.OrderVisualisationViewModel(laCommande);
        }

        private void but_Retour_Click(object sender, RoutedEventArgs e)
        {
            OrdersViewModel ordersViewModel = new OrdersViewModel(); // Instancier un ViewModel valide
            OrdersPage page = new OrdersPage(ordersViewModel);
            NavigationService.Navigate(page);
        }

        private void but_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.butSupprimer_Click(laCommande);
            but_Retour_Click(sender, e);

        }

        public void SetPriceTTC()
        {
            int OrderPrice = 0;
            lab_PxTotal.Content = "Total TTC: " + ViewModel.ResolvePriceTTC().ToString()  +"€";
        }
    }
}
