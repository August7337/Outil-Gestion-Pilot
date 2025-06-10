using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui.Abstractions.Controls;


namespace Outil_Gestion_Pilot.Views.Pages
{
    public partial class OrdersPage : INavigableView<OrdersViewModel>
    {
        public OrdersViewModel ViewModel { get; }

        public OrdersPage(OrdersViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void but_visualiser_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}