using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Windows.Data;
using System.Windows.Controls;

namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public partial class DealersViewModel : ObservableObject
    {
        [ObservableProperty]
        private double searchDealerId;
        [ObservableProperty]
        private string searchReseller;

        public ICollectionView DealersView { get; set; }

        public DealersViewModel()
        {
            DealersView = CollectionViewSource.GetDefaultView(Reseller.Resellers);
            DealersView.Filter = CombinedFilter;
        }


        /// <summary>
        /// Group the all the filter to use all at the same time
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool CombinedFilter(object item)
        {
            if (item is Reseller dealer)
            {
                return ResellerSearch(dealer) && DealersIdSearch(dealer);
            }
            return false;
        }
        /// <summary>
        /// Refreshes the data grid when the function is called.
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchResellerChanged(string value)
        {
            DealersView.Refresh();
        }

        /// <summary>
        /// finds the dealers in the resellers collection that starts with the text of the obj.
        /// </summary>
        /// <param reseller="obj"></param>
        /// <returns></returns>
        private bool ResellerSearch(object obj)
        {
            if (obj is not Reseller dealer) return false;
            if (string.IsNullOrWhiteSpace(SearchReseller))
                return true;

            return dealer.RaisonSociale.StartsWith(SearchReseller, StringComparison.OrdinalIgnoreCase);
        }


        private string searchDealerIdText;
        public string SearchDealerIdText
        {
            get => searchDealerIdText;
            set
            {
                searchDealerIdText = value;

                if (string.IsNullOrWhiteSpace(SearchDealerIdText))
                {
                    SearchDealerId = -10;
                    DealersView.Refresh();
                }
                else
                {

                    if (int.TryParse(value, out int dealerId))
                    {
                        SearchDealerId = dealerId;
                        DealersView.Refresh();
                    }
                }

                OnPropertyChanged(nameof(SearchDealerIdText));
            }
        }



        /// <summary>
        /// Refreshes the data grid when the function is called.
        /// </summary>
        /// <param name="value"></param>
        public void OnSearchChanged(double value)
        {
            DealersView.Refresh();
        }

        /// <summary>
        /// filter of dealerId
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool DealersIdSearch(object obj)
        {
            if (obj is not Reseller dealer) return false;
            if (SearchDealerId <= 0)
                return true;

            return dealer.NumeroRevendeur == SearchDealerId;
        }
    }
}

