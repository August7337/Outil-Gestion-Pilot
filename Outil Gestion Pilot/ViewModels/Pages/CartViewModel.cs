using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Windows.Data;
using Outil_Gestion_Pilot.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Outil_Gestion_Pilot.Services;

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
            CartView.Filter = CodeSearch;
            CartView.Filter = PriceSearch;

        }

        private string searchPriceText;
        public string SearchPriceText
        {
            get => searchPriceText;
            set
            {
                searchPriceText = value;

                if (double.TryParse(value, out double price)) // Vérifie si la conversion est valide
                {
                    SearchPrice = price; // Met à jour la vraie valeur numérique
                    CartView.Refresh(); // Rafraîchit la liste filtrée
                }

                OnPropertyChanged(nameof(SearchPriceText)); // Notifie la vue du changement
            }
        }

        [ObservableProperty]
        private double searchPrice; // La vraie valeur numérique utilisée pour le filtrage

        /// <summary>
        /// Refreshes the data grid when the function is called.
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchCodeChanged(string value)
        {
            CartView.Refresh();
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
        partial void OnSearchPriceChanged(double value)
        {
            CartView.Refresh();
        }

        private bool PriceSearch(object obj)
        {
            if (obj is not Product product) return false;
            if (double.IsNaN(SearchPrice) || SearchPrice <= 0)
                return true; // Affiche tous les produits si aucun prix valide n'est saisi.

            return product.SellingPrice >= SearchPrice; 
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
                Color = new List<ProductColor> { ProductColor.Noire }
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
                Color = new List<ProductColor> { ProductColor.Vert }
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
                Color = new List<ProductColor> { ProductColor.Bleu, ProductColor.Vert }
            });
        }

        public void AddProduct(Product product)
        {
            Carts.Add(product);
        }

        public void butNvxRenvedeur_Click(object sender, EventArgs e)
        {
            DealerWindow dealerWindow = new DealerWindow();
            dealerWindow.ShowDialog();

        }
    }
}