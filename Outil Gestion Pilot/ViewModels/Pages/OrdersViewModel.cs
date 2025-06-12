using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Windows.Data;

namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public partial class OrdersViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Order> orders;

        [ObservableProperty]
        private double searchCommand;
        [ObservableProperty]
        private string searchReseller;
        private string searchDate = null;

        public ICollectionView OrdersView { get; set; }


        public string SearchDate
        {
            get
            {
                return this.searchDate;
            }

            set
            {
                this.searchDate = value;
                OnPropertyChanged(nameof(SearchDate));
                OrdersView.Refresh();
            }
        }



        public OrdersViewModel()
        {
            Orders = new ObservableCollection<Order>(new Order().FindAll());
            OrdersView = CollectionViewSource.GetDefaultView(Orders);
            OrdersView.Filter = CombinedFilter;
        }

        /// <summary>
        /// Group the all the filter to use all at the same time
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool CombinedFilter(object item)
        {
            if (item is Order order)
            {
                return ResellerSearch(order) && DateSearch(order) && CommandSearch(order);
            }
            return false;
        }
        /// <summary>
        /// Refreshes the data grid when the function is called.
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchResellerChanged(string value)
        {
            OrdersView.Refresh();
        }

        /// <summary>
        /// finds the products in the product collection that starts with the text of the obj.
        /// </summary>
        /// <param reseller="obj"></param>
        /// <returns></returns>
        private bool ResellerSearch(object obj)
        {
            if (obj is not Order order) return false;
            if (string.IsNullOrWhiteSpace(SearchReseller))
                return true;

            return order.Reseller.StartsWith(SearchReseller, StringComparison.OrdinalIgnoreCase);
        }



        public void OnSearchDateChanged(string? value)
        {
            OrdersView.Refresh();
        }
        /// <summary>
        /// Filter of date
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool DateSearch(object obj)
        {
            if (obj is not Order order) return false;
            if (string.IsNullOrWhiteSpace(SearchDate)) return true; // Aucun filtre appliqué

            // Essayer de convertir SearchDate en DateTime
            if (DateTime.TryParse(SearchDate, out DateTime parsedDate))
            {
                return order.OrderDate.Year == parsedDate.Year  // Filtre par année
                    && order.OrderDate.Month == parsedDate.Month // Filtre par mois
                    && order.OrderDate.Day == parsedDate.Day;   // Filtre par jour
            }

            return false; // Si la conversion échoue, ignorer cet élément
        }

        /// <summary>
        /// Class to change the string in the textbox into an int to filter the command.
        /// </summary>
        private string searchCommandText;
        public string SearchCommandText
        {
            get => searchCommandText;
            set
            {
                searchCommandText = value;

                if (string.IsNullOrWhiteSpace(SearchCommandText))
                {
                    SearchCommand = -10;
                    OrdersView.Refresh();
                }
                else
                {

                    if (int.TryParse(value, out int commandId))
                    {
                        SearchCommand = commandId;
                        OrdersView.Refresh();
                    }
                }

                OnPropertyChanged(nameof(SearchCommandText));
            }
        }



        /// <summary>
        /// Refreshes the data grid when the function is called.
        /// </summary>
        /// <param name="value"></param>
        public void OnSearchcommandIdChanged(double value)
        {
            OrdersView.Refresh();
        }

        /// <summary>
        /// filter of command id 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CommandSearch(object obj)
        {
            if (obj is not Order order) return false;
            if (SearchCommand <= 0)
                return true;

            return order.CommandeId == SearchCommand;

        }

        public void AddProduct(Order order)
        {
            Orders.Add(order);
        }
    }
}

