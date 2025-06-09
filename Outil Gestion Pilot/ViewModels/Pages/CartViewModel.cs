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

namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public partial class CartViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Product> carts;

        public ICollectionView CartView { get; set; }
        [ObservableProperty]
        private string searchCode;

        public CartViewModel()
        {
            Carts = new ObservableCollection<Product>();
            LoadSampleProducts();

            CartView = CollectionViewSource.GetDefaultView(Carts);
            CartView.Filter = CombinedFilter;

        }

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
        /// finds the products in the product collection that starts with the text of the obj.
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
            if (obj is not Product product) return false;
            if (SearchPrice <= 0)
                return true; // Return all the products if the Textbox is empty 

            return product.SellingPrice == SearchPrice; 
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
            if (obj is not Product product) return false;
            if (SearchQte <= 0)
                return true; // Return all the products if the Textbox is empty

            return product.DesiredQuantity == SearchQte;
        }


        /// <summary>
        /// Loads the data. temporary.
        /// </summary>
        private void LoadSampleProducts()
        {
            Carts.Add(new Product
            {
                ImagePath = "/Images/product1.jpg",
                Code = "P001",
                Name = "Produit 1",
                Category = ProductCategory.Bureau,
                Type = ProductType.Bille,
                Tipe = ProductTipe.Epaisse,
                SellingPrice = 25.99,
                Stock = 100,
                Color = new List<ProductColor> { ProductColor.Noire },
                DesiredQuantity = 20
            });

            Carts.Add(new Product
            {
                ImagePath = "/Images/product2.jpg",
                Code = "P002",
                Name = "Produit 2",
                Category = ProductCategory.Bureau,
                Type = ProductType.Plume,
                Tipe = ProductTipe.Fine,
                SellingPrice = 45.50,
                Stock = 50,
                Color = new List<ProductColor> { ProductColor.Vert },
                DesiredQuantity = 200
            });

            Carts.Add(new Product
            {
                ImagePath = "/Images/product3.jpg",
                Code = "P003",
                Name = "Produit 3",
                Category = ProductCategory.Loisir,
                Type = ProductType.Roller_gel,
                Tipe = ProductTipe.Moyenne,
                SellingPrice = 15.75,
                Stock = 200,
                Color = new List<ProductColor> { ProductColor.Bleu, ProductColor.Vert },
                DesiredQuantity = 100
            });
        }

        public void AddProduct(Product product)
        {
            Carts.Add(product);
        }

        public double ResolvePriceTTC() // a terminer
        {
            double price = 0;
            return price;

        }
        public void butNvxRenvedeur_Click(object sender, EventArgs e)
        {
            DealerWindow dealerWindow = new DealerWindow();
            dealerWindow.ShowDialog();

        }
    }
}