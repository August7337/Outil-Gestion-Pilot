using Microsoft.Extensions.DependencyInjection;
using Outil_Gestion_Pilot.Services;
using Outil_Gestion_Pilot.ViewModels.Windows;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.Views.Windows
{
    public partial class ConnectionWindow : FluentWindow
    {
        public ConnectionWindowViewModel ViewModel { get; set; }

        public ConnectionWindow()
        {
            InitializeComponent();
            ViewModel = App.Services.GetRequiredService<ConnectionWindowViewModel>();
            DataContext = ViewModel;
        }

        private void Validation_Click(object sender, RoutedEventArgs e)
        {
            ErrorTxt.Text = ViewModel.Connection(usernameBox.Text, passwordBox.Password);
            if (SessionService.Instance.Login is not null)
                this.DialogResult = true;
        }
    }
}
