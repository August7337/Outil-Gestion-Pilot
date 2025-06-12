using Microsoft.Extensions.DependencyInjection;
using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.Services;
using Outil_Gestion_Pilot.ViewModels.Pages;
using Outil_Gestion_Pilot.ViewModels.Windows;
using System.Configuration;
using System.Windows;
using System.Windows.Media.Animation;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.Views.Windows
{
    public enum Action { Modifier, Créer }

    public partial class DealerWindow : FluentWindow
    {
        public DealerWindowViewModel ViewModel { get; set; }
        private readonly SessionService sessionService;

        public DealerWindow(Action action, Reseller aReseller)
        {
            this.DataContext = aReseller;
            InitializeComponent();
            but_Revendeur.Content = action;
        }

        private void but_Revendeur_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
