﻿using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;
using MessageBox = System.Windows.MessageBox;


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

        public OrdersPage()
        {
        }

        private void but_visualiser_Click(object sender, RoutedEventArgs e)
        {
            if(OrderDG.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une commande à visualiser.", "Aucune commande sélectionnée", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Order SelectedOrder = (Order)OrderDG.SelectedItem;
                Order copie = new Order(SelectedOrder.CommandeId, SelectedOrder.Reseller, SelectedOrder.OrderDate, SelectedOrder.Delivery);
                OrderVisualisationPage page = new OrderVisualisationPage(copie);
                NavigationService.Navigate(page);

            }
        }
    }
}