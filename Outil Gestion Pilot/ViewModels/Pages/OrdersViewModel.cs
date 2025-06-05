using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Windows.Data;
namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public partial class OrdersViewModel : ObservableObject
    {
        public ICollectionView OrdersView { get; set; } 
        [ObservableProperty]
         private ObservableCollection<Order> orders;

         public OrdersViewModel()
         {
             Orders = new ObservableCollection<Order>();
             LoadSampleOrders();
            OrdersView = CollectionViewSource.GetDefaultView(Orders);
         }

         private void LoadSampleOrders()
         {
             Orders.Add(new Order
             {
                 Reseller = "CASINO Lyon",
                 OrderDate = new DateTime(2025, 5, 23, 11, 55, 0),
             });

             Orders.Add(new Order
             {
                 Reseller = "CASINO Paris",
                 OrderDate = DateTime.Now,

             });
         }
        public void AddProduct(Order order)
        {
            Orders.Add(order);
        }
    }
}
