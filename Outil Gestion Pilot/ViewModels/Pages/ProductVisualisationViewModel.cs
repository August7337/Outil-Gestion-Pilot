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
    public partial class ProductVisualisationViewModel : ObservableObject
    {
        private Product product;

        public ProductVisualisationViewModel(Product product)
        {
            Product = product;
        }

        public Product Product
        {
            get { return this.product; }
            set { this.product = value; }
        }
    }
}
