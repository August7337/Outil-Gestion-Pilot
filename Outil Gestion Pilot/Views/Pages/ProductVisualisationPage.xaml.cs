using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using Outil_Gestion_Pilot.Views.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Wpf.Ui;

namespace Outil_Gestion_Pilot.Views.Pages
{
    public partial class ProductVisualisationPage : Page
    {
        INavigationService navigationService;

        private Product product;

        public ProductVisualisationPage(Product product)
        { 
            InitializeComponent();
            this.DataContext = new ViewModels.Pages.ProductVisualisationViewModel(product);
            this.Product = product;
        }

        public Product Product
        {
            get { return this.product; }
            set { this.product = value; }
        }

        private void But_Retour_Click(object sender, RoutedEventArgs e)
        {
            ProductsViewModel productsViewModel = new ProductsViewModel();
            ProductsPage page = new ProductsPage(productsViewModel);
            NavigationService.Navigate(page);
        }

        private void ModifyBtn_Click(object sender, RoutedEventArgs e)
        {
            SplashScreenProductWindow window = new SplashScreenProductWindow(ProductAction.Modifier, this.Product);
            if (window.ShowDialog() == true)
            {
                try
                {
                    this.Product.Update();
                    ProductVisualisationPage page = new ProductVisualisationPage(this.Product);
                    NavigationService.Navigate(page);
                    NavigationService.RemoveBackEntry();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur");
                }
            }
        }
    }
}
