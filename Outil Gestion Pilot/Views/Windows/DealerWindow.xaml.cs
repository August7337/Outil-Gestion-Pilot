using Outil_Gestion_Pilot.Models;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.Views.Windows
{
    public enum Action { Modifier, Créer }

    public partial class DealerWindow : FluentWindow
    {
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
