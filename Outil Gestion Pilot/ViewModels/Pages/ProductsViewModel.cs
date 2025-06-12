using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Windows.Data;
using System.Diagnostics;
using Outil_Gestion_Pilot.Views.Windows;

namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public partial class ProductsViewModel : ObservableObject
    {
        private List<Product> products;

        [ObservableProperty]
        private Product selectedProduct;


        public ICollectionView ProductsView { get; set; }

        public List<Product> Products
        {
            get { return Product.Products; }
            set { Product.Products = value; }
        }

        [ObservableProperty]
        private string searchCode;

        public ProductsViewModel()
        {
            ProductsView = CollectionViewSource.GetDefaultView(Products);
            ProductsView.Filter = CodeSearch;
        }

        /// <summary>
        /// Refreshes the data grid when the function is called.
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchCodeChanged(string value)
        {
            ProductsView.Refresh();
        }

        /// <summary>
        /// Finds the products in the product collection that starts with the text of the obj.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CodeSearch(object obj)
        {
            if (obj is not Product product) return false;
            if (string.IsNullOrWhiteSpace(SearchCode))
                return true;
        
            return product.Code.StartsWith(SearchCode, StringComparison.OrdinalIgnoreCase);
        }

        internal void AddToCart()
        {
            if (SelectedProduct != null)
            {
                SelectQuantityWindow select = new SelectQuantityWindow();
                bool? result = select.ShowDialog();
                if (result == true)
                    Cart.Products.Add(new OrderedProduct(select.Quantity, 0, SelectedProduct));
            }
            else
            {
                var uiMessageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "Pilot - Attention",
                    Content = "Aucun produit sélectionné.",
                };

                uiMessageBox.ShowDialogAsync();
            }
        }
    }
}