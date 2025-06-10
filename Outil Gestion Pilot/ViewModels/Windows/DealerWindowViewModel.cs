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
        private readonly SessionService sessionService;

        public ICollectionView DealerView { get; set; }

        public DealerWindowViewModel(SessionService sessionService)
        {
            this.sessionService = sessionService;
        }


        public DealerWindowViewModel()
        {
            Dealers = new ObservableCollection<Reseller>();
            LoadSampleReseller();

            DealerView = CollectionViewSource.GetDefaultView(Dealers);

        }

        private void LoadSampleReseller()
        {
            Dealers.Add(new Reseller
            {
                NumeroRevendeur = 1,
                RaisonSociale = "SAS Carrefour",
                Rue = "9 rue de l'arc en ciel",
                Cp = "74000",
                Ville = "Annecy"
            });

            Dealers.Add(new Reseller
            {
                NumeroRevendeur = 2,
                RaisonSociale = "Super U",
                Rue = "2 chemin du fier",
                Cp = "38110",
                Ville = "Grenoble"
            });

            Dealers.Add(new Reseller
            {
                NumeroRevendeur = 3,
                RaisonSociale = "Intermarché Lugrin",
                Rue = "ZAC des crets",
                Cp = "74500",
                Ville = "Lugrin"
            });
        }
    }
}
