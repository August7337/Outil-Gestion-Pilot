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
    public partial class SelectQuantityWindow : FluentWindow
    {
        public SelectQuantityWindowViewModel ViewModel { get; set; }
        private int quantity;

        public SelectQuantityWindow()
        {
            InitializeComponent();
            ViewModel = App.Services.GetRequiredService<SelectQuantityWindowViewModel>();
            DataContext = ViewModel;
        }

        public int Quantity
        {
            get { return this.quantity; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("The valeu must be greater than 0!");
                this.quantity = value;
            }
        }

        private void Validation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Quantity = int.Parse(quantityBox.Text);
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ErrorTxt.Text = ex.Message;
            }
            
        }
    }
}
