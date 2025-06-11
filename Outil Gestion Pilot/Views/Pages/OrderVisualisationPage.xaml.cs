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

namespace Outil_Gestion_Pilot.Views.Pages
{
    /// <summary>
    /// Logique d'interaction pour OrderVisualisationPage.xaml
    /// </summary>
    public partial class OrderVisualisationPage : Page
    {
        public OrderVisualisationPage()
        { 
            InitializeComponent();
            DataContext = new ViewModels.Pages.OrderVisualisationViewModel();
        }

        private void but_Retour_Click(object sender, RoutedEventArgs e)
        {
            Uri pageFunctionUri = new Uri("Views/Pages/OrdersPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(pageFunctionUri);
        }
    }
}
