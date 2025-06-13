using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using Outil_Gestion_Pilot.ViewModels.Windows;
using System.Windows.Interop;
using Wpf.Ui.Abstractions.Controls;

namespace Outil_Gestion_Pilot.Views.Pages
{
    public partial class CartPage : INavigableView<CartViewModel>
    {
        public CartViewModel ViewModel { get; }

        public CartPage(CartViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
            SetPriceTTC();
            
        }
        public void SetPriceTTC()
        {
            labPrixTotal.Content = "Total TTC: " + ViewModel.ResolvePriceTTC().ToString() + "€";
        }

       
        private void ButCommander_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}