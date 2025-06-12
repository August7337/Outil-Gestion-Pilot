using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui.Abstractions.Controls;

namespace Outil_Gestion_Pilot.Views.Pages
{
    public partial class ProductsPage : INavigableView<ProductsViewModel>
    {
        public ProductsViewModel ViewModel { get; }
        

        public ProductsPage(ProductsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
            ViewModel.InitializeRoleBtn(CartBtn, ViewBtn, NewProductBtn);
        }

        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddToCart();
        }

        private void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowProduct(NavigationService, productsDG);
        }

        
    }
}