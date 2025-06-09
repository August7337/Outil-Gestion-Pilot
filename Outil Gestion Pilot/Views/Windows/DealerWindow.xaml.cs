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
    public partial class DealerWindow : FluentWindow
    {
        public DealerWindowViewModel ViewModel { get; set; }
        private readonly SessionService sessionService;

        public DealerWindow()
        {
            InitializeComponent();
            ViewModel = App.Services.GetRequiredService<DealerWindowViewModel>();
            DataContext = ViewModel;
            this.sessionService = sessionService;
        }
    }
}
