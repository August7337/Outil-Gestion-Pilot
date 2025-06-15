using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.Models.Attributes;
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
