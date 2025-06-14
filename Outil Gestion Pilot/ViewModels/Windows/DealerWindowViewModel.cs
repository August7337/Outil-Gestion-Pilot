using Npgsql;
using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.Services;
using Outil_Gestion_Pilot.Views.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.ViewModels.Windows
{
    public partial class DealerWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Reseller> dealers;

        public ICollectionView DealerView { get; set; }

        public DealerWindowViewModel()
        {
            Dealers = new ObservableCollection<Reseller>();

            DealerView = CollectionViewSource.GetDefaultView(Dealers);
        }
    }
}
