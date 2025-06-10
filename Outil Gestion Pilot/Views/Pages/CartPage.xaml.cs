using Outil_Gestion_Pilot.ViewModels.Pages;
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

        private void butCommander_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butNvxRenvedeur_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.butNvxRenvedeur_Click(sender, e);
        }

        

        private void butModRevendeur_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.butModifyRenvedeur_Click(sender, e);
        }
    }
}