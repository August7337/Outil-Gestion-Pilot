using Microsoft.Extensions.DependencyInjection;
using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.Models.Attributes;
using Outil_Gestion_Pilot.Services;
using Outil_Gestion_Pilot.ViewModels.Windows;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media.Animation;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.Views.Windows
{
    public enum ProductAction { Modifier, Créer }

    public partial class SplashScreenProductWindow : FluentWindow
    {
        public SplashScreenProductWindow(ProductAction action, Product product)
        {
            this.DataContext = product;
            InitializeComponent();
            ValidationBtn.Content = action;
            this.TypeComboBox.ItemsSource = Models.Attributes.Type.FindAll();
            this.TipeComboBox.ItemsSource = Tipe.Tipes;
        }

        private void ValidationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
