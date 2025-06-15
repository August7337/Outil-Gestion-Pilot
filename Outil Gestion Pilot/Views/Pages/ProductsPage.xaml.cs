using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using Outil_Gestion_Pilot.Views.Windows;
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

        private void NewProductBtn_Click(object sender, RoutedEventArgs e)
        {
            

            Product product = new Product();
            SplashScreenProductWindow window = new SplashScreenProductWindow(ProductAction.Créer, product);
            if (window.ShowDialog() == true)
            {
                try
                {
                    Product.Products.Add(product);
                    product.Create();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Erreur");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur");
                }
                ViewModel.ProductsView.Refresh();
            }
        }
    }
}