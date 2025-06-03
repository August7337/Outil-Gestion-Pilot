using Outil_Gestion_Pilot.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace Outil_Gestion_Pilot.Views.Pages
{
    public partial class OrdersPage : INavigableView<OrdersViewModel>
    {
        public OrdersViewModel ViewModel { get; }

        public OrdersPage(OrdersViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}