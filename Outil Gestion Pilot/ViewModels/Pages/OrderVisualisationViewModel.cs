using Outil_Gestion_Pilot.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public partial class OrderVisualisationViewModel:ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<OrderedProduct> orderedProducts;

        public ICollectionView OrderedProductView { get; set; }

        public OrderVisualisationViewModel() 
        {
            OrderedProducts = new ObservableCollection<OrderedProduct>(OrderedProduct.FindAll());
            OrderedProductView = CollectionViewSource.GetDefaultView(OrderedProducts);
        }
    }
}
