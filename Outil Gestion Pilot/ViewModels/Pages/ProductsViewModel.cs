using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Windows.Data;
using System.Diagnostics;
using Outil_Gestion_Pilot.Views.Windows;
using Outil_Gestion_Pilot.Services;
using Wpf.Ui.Controls;
using Outil_Gestion_Pilot.Views.Pages;
using System.Windows.Navigation;
using System.Windows.Controls;

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
            ProductsView.Filter = CombinedFilter;
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

        internal void InitializeRoleBtn(Wpf.Ui.Controls.Button cartBtn, Wpf.Ui.Controls.Button viewBtn, Wpf.Ui.Controls.Button newProductBtn)
        {
            if (SessionService.Instance.Role == "Responsable production")
            {
                cartBtn.Visibility = Visibility.Collapsed;
            }
            else if (SessionService.Instance.Role == "Commercial")
            {
                viewBtn.Visibility = Visibility.Collapsed;
                newProductBtn.Visibility = Visibility.Collapsed;
            }
        }

        internal void ShowProduct(NavigationService navigationService, System.Windows.Controls.DataGrid dataGrid)
        {
            if (dataGrid.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Veuillez sélectionner une commande à visualiser.", "Aucune commande sélectionnée", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Product SelectedProduct = (Product)dataGrid.SelectedItem;
                Product copie = new Product(SelectedProduct.Id, SelectedProduct.ImagePath, SelectedProduct.Code, SelectedProduct.Name, SelectedProduct.Type, SelectedProduct.Tipe, SelectedProduct.SellingPrice, SelectedProduct.Stock, SelectedProduct.Color, SelectedProduct.Disponibility);
                ProductVisualisationPage page = new ProductVisualisationPage(copie);
                navigationService.Navigate(page);
                
            }
        }

        //-----------------------------------------
        //FILTERS
        //
        private string searchPxText;
        [ObservableProperty]
        private double searchPx;
        [ObservableProperty]
            private int searchQte; 



        /// <summary>
        /// Group the all the filter to use all at the same time
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool CombinedFilter(object item)
        {
            if (item is Product product)
            {
                return CodeSearch(product) && PriceSearch(product) && QteSearch(product);
            }
            return false;
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


        public string SearchPxText
        {
            get => searchPxText;
            set
            {
                searchPxText = value;

                if (string.IsNullOrWhiteSpace(SearchPxText))
                {
                    SearchPx = -10; // Set to a negative value to indicate no search
                    ProductsView.Refresh();
                }
                else
                {
                    string doubleFormat = value.Replace('.', ',');

                    if (double.TryParse(doubleFormat, out double price))
                    {
                        SearchPx = price;
                        ProductsView.Refresh();
                    }
                }

                OnPropertyChanged(nameof(SearchPxText));
            }
        }

       

        /// <summary>
        /// Refreshes the data grid when the function is called.
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchPxChanged(double value)
        {
            ProductsView.Refresh();
        }

        private bool PriceSearch(object obj)
        {
            if (obj is not Product product) return false;
            if (SearchPx <= 0)
                return true;

            return product.SellingPrice == SearchPx;
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
                    SearchQte = -10;
                    ProductsView.Refresh();

                }
                else if (int.TryParse(value, out int quantity))
                {
                    SearchQte = quantity;
                    ProductsView.Refresh();

                }

                OnPropertyChanged(nameof(SearchQteText));
            }
        }


        public void OnSearchQteChanged(double value)
        {
            ProductsView.Refresh();
        }

        private bool QteSearch(object obj)
        {
            if (obj is not Product product) return false;
            if (SearchQte <= 0)
                return true;

            return product.Stock == SearchQte;
        }
    }
}