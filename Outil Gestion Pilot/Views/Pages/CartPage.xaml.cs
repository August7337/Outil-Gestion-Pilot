using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using Outil_Gestion_Pilot.ViewModels.Windows;
using System.Collections.ObjectModel;
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
        }

        private void ButOrder_Click(object sender, RoutedEventArgs e)
        {
            Cart.Transport = lstLivraison.SelectedIndex + 1;
            Cart.Resseller = lstRevendeur.SelectedIndex + 1;
            Cart.OrderDate = DateTime.Now;

            ViewModel.PurchaseCart();
        }
    }
}