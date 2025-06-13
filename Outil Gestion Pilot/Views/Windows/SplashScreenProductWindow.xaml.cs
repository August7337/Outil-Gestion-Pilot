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
    public partial class SplashScreenProductWindow : FluentWindow
    {
        public SplashScreenProductWindowViewModel ViewModel { get; set; }

        public SplashScreenProductWindow()
        {
            InitializeComponent();
            ViewModel = App.Services.GetRequiredService<SplashScreenProductWindowViewModel>();
            DataContext = ViewModel;
        }
    }
}
