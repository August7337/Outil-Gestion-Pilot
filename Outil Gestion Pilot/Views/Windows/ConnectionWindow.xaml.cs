using Outil_Gestion_Pilot.ViewModels.Windows;
using System.Windows;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.Views.Windows
{
    public partial class ConnectionWindow : FluentWindow
    {
        public ConnectionWindowViewModel ViewModel { get; set; }

        public ConnectionWindow()
        {
            InitializeComponent();
            ViewModel = new ConnectionWindowViewModel();
            DataContext = ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
