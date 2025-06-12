using Microsoft.Extensions.DependencyInjection;
using Outil_Gestion_Pilot.Services;
using Outil_Gestion_Pilot.ViewModels.Windows;
using System.Configuration;
using System.Windows;
using System.Windows.Media.Animation;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.Views.Windows
{
    public partial class ConnectionWindow : FluentWindow
    {
        public ConnectionWindowViewModel ViewModel { get; set; }
        //private readonly SessionService sessionService;

        public ConnectionWindow()
        {
            InitializeComponent();
            ViewModel = App.Services.GetRequiredService<ConnectionWindowViewModel>();
            DataContext = ViewModel;
            //this.sessionService = sessionService;

            if (ConfigurationManager.AppSettings["NeedAuth"] == "false")
            {
                SessionService.Instance.Login = ConfigurationManager.AppSettings["Login"];
                this.Loaded += (s, e) =>
                {
                    this.DialogResult = true;
                    this.Close();
                };
            }
        }

        private void Validation_Click(object sender, RoutedEventArgs e)
        {
            ErrorTxt.Text = ViewModel.Connection(usernameBox.Text, passwordBox.Password);
            if (SessionService.Instance.Login is not null)
                this.DialogResult = true;
        }
    }
}
