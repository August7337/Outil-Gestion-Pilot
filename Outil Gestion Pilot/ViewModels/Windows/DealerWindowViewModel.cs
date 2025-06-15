using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

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
