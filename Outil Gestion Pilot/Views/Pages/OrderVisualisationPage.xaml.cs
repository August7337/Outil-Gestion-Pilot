using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui;

namespace Outil_Gestion_Pilot.Views.Pages
{
    /// <summary>
    /// Logique d'interaction pour OrderVisualisationPage.xaml
    /// </summary>
    public partial class OrderVisualisationPage : Page
    {
        INavigationService navigationService;

        public OrderVisualisationPage(Order uneCommande)
        { 
            InitializeComponent();
            this.DataContext = new ViewModels.Pages.OrderVisualisationViewModel(uneCommande);
        }

        private void but_Retour_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
