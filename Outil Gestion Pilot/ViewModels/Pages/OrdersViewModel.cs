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
       
        public ICollectionView OrdersView { get; set; }

        [ObservableProperty]
        private string searchReseller;


        public OrdersViewModel()
         {
             Orders = new ObservableCollection<Order>(new Order().FindAll());
            OrdersView = CollectionViewSource.GetDefaultView(Orders);
            OrdersView.Filter = ResellerSearch;
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
       
        public void AddProduct(Order order)
        {
            Orders.Add(order);
        }
    }
}
