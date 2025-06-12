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
    public partial class ProductVisualisationPage : Page
    {
        INavigationService navigationService;

        public ProductVisualisationPage(Product product)
        { 
            InitializeComponent();
            this.DataContext = new ViewModels.Pages.ProductVisualisationViewModel(product);
        }

        public void but_Retour_Click()
        {

        }
    }
}
