using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Models
{
    public class Cart
    {
        public static ObservableCollection<OrderedProduct> Products = new ObservableCollection<OrderedProduct>();
    }
}
