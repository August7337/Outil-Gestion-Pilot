using System.Collections.ObjectModel;

namespace Outil_Gestion_Pilot.Models
{
    public class Cart
    {
        public static ObservableCollection<OrderedProduct> Products = new ObservableCollection<OrderedProduct>();
        public static int Transport;
        public static int Resseller;
        public static DateTime OrderDate;
    }
}
