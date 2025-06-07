using Outil_Gestion_Pilot.ViewModels.Pages;
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

        private void butCommander_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}