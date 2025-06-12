using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Windows.Data;
using Outil_Gestion_Pilot.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Outil_Gestion_Pilot.Services;
using Outil_Gestion_Pilot.Views.Pages;
using Outil_Gestion_Pilot.Models.Attributes;
using Outil_Gestion_Pilot.ViewModels.Windows;

namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public partial class CartViewModel : ObservableObject
    {
        public ObservableCollection<OrderedProduct> Carts => Cart.Products;

        public ICollectionView CartView { get; set; }
        [ObservableProperty]
        private string searchCode;

        public CartViewModel()
        {
            CartView = CollectionViewSource.GetDefaultView(Cart.Products);
            CartView.Filter = CombinedFilter;
        }

        /// <summary>
        /// Group the all the filter to use all at the same time
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool CombinedFilter(object item)
        {
            if (item is OrderedProduct ordered)
            {
                return CodeSearch(ordered) && PriceSearch(ordered) && QteSearch(ordered);
            }
            return false;
        }

        /// <summary>
        /// finds the products in the product collection that starts with the text of the obj.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CodeSearch(object obj)
        {
            if (obj is not OrderedProduct ordered) return false;
            if (string.IsNullOrWhiteSpace(SearchCode))
                return true;

            return ordered.Product.Code.StartsWith(SearchCode, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Refreshes the data grid when the function is called.
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchCodeChanged(string value)
        {
            CartView.Refresh();
        }

        /// <summary>
        /// Filter of SellingPrice
        /// </summary>
        private string searchPriceText;
        public string SearchPriceText
        {
            get => searchPriceText;
            set
            {
                searchPriceText = value;

                if (string.IsNullOrWhiteSpace(SearchPriceText))
                {
                    SearchPrice = -10; // If the textbox is empty, the variable SearchPrice is set to -10 
                    CartView.Refresh() ;
                }
                else
                {
                    string doubleFormat = value.Replace('.', ','); // The "." are interpreted as ","

                    if (double.TryParse(doubleFormat, out double price)) // Check if the conversion is valid
                    {
                        SearchPrice = price; // Set up the value write in the textbox
                        CartView.Refresh(); // Refresh the filtered list
                    }
                }
                
                OnPropertyChanged(nameof(SearchPriceText)); 
            }
        }

        [ObservableProperty]
        private double searchPrice; // This variable is used in the SearchPrice method, type: double

        /// <summary>
        /// Refreshes the data grid when the function is called.
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchPriceChanged(double value)
        {
            CartView.Refresh();
        }

        private bool PriceSearch(object obj)
        {
            if (obj is not OrderedProduct ordered) return false;
            if (SearchPrice <= 0)
                return true;

            return ordered.Product.SellingPrice == SearchPrice;
        }

        /// <summary>
        /// Filter of Quantity
        /// </summary>
        private string searchQteText;
        public string SearchQteText
        {
            get => searchQteText;
            set
            {
                searchQteText = value;

                if (string.IsNullOrWhiteSpace(SearchQteText))
                {
                    SearchQte = -10; // If the textbox is empty, the variable SearchPrice is set to -10 
                    CartView.Refresh(); // Refresh the filtered list

                }
                else if (int.TryParse(value, out int quantity)) 
                {
                    SearchQte = quantity; // Met à jour la vraie valeur numérique
                    CartView.Refresh(); // Refresh the filtered list

                }

                OnPropertyChanged(nameof(SearchQteText)); 
            }
        }

        [ObservableProperty]
        private double searchQte; // This variable is used in the SearchQte method, type: double

        partial void OnSearchQteChanged(double value)
        {
            CartView.Refresh();
        }

        private bool QteSearch(object obj)
        {
            if (obj is not OrderedProduct ordered) return false;
            if (SearchQte <= 0)
                return true;

            return ordered.Product.DesiredQuantity == SearchQte;
        }

        public double ResolvePriceTTC() 
        {
            double price = 0;
            foreach (OrderedProduct aproduct in Carts)
            {
                price += aproduct.Product.SellingPrice * aproduct.Product.DesiredQuantity;
            }
            return price;
        }

        public void butNvxRenvedeur_Click(object sender, EventArgs e)
        {
            DealerWindow dealerWindow = new DealerWindow(Views.Windows.Action.Créer);
            dealerWindow.ShowDialog();

        }
        public void butModifyRenvedeur_Click(object sender, EventArgs e)
        {
            DealerWindow dealerWindow = new DealerWindow(Views.Windows.Action.Modifier);
            dealerWindow.ShowDialog();

        }
    }
}