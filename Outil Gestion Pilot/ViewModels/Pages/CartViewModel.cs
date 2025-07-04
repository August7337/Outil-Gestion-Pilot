﻿using Npgsql;
using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Data;

namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public enum ModeLivraison { UPS, Chronopost, Relais };

    public partial class CartViewModel : ObservableObject
    {

        public ICollectionView CartView { get; set; }
        [ObservableProperty]
        private string searchCode;

        public ObservableCollection<Reseller> Resellers => Reseller.resellers;

        public CartViewModel()
        {
            CartView = CollectionViewSource.GetDefaultView(Cart.Products);
            CartView.Filter = CombinedFilter;
            Cart.Products.CollectionChanged += (s, e) => OnPropertyChanged(nameof(PriceTTC));
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
                    SearchPrice = -10;
                    CartView.Refresh() ;
                }
                else
                {
                    string doubleFormat = value.Replace('.', ',');

                    if (double.TryParse(doubleFormat, out double price))
                    {
                        SearchPrice = price;
                        CartView.Refresh();
                    }
                }
                
                OnPropertyChanged(nameof(SearchPriceText)); 
            }
        }

        [ObservableProperty]
        private double searchPrice;

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
                    CartView.Refresh();

                }
                else if (int.TryParse(value, out int quantity)) 
                {
                    SearchQte = quantity;
                    CartView.Refresh();

                }

                OnPropertyChanged(nameof(SearchQteText)); 
            }
        }

        [ObservableProperty]
        private double searchQte;

        partial void OnSearchQteChanged(double value)
        {
            CartView.Refresh();
        }

        /// <summary>
        /// Filter of Quantity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
            foreach (OrderedProduct aproduct in Cart.Products)
            {
                price += aproduct.Product.SellingPrice * aproduct.Quantity;
            }
            return price;
        }

        public double PriceTTC
        {
            get => ResolvePriceTTC();
        }

        /// <summary>
        /// SQL query to create a new order in the database.
        /// </summary>
        public void Create()
        {
            try
            {
                using (NpgsqlCommand cmdInsert = new NpgsqlCommand("INSERT INTO commande (numemploye, numrevendeur, datecommande, numtransport, datelivraison, prixtotal) VALUES (@numemploye, @numrevendeur, @datecommande, @numtransport, @datelivraison, @prixtotal);"))
                {
                    cmdInsert.Parameters.AddWithValue("@numemploye", 1);
                    cmdInsert.Parameters.AddWithValue("@numtransport", Cart.Transport);
                    cmdInsert.Parameters.AddWithValue("@numrevendeur", Cart.Resseller);
                    cmdInsert.Parameters.AddWithValue("@datecommande", Cart.OrderDate);
                    cmdInsert.Parameters.AddWithValue("@datelivraison", new DateTime(1, 1, 1));
                    cmdInsert.Parameters.AddWithValue("@prixtotal", 200);

                    DataAccess.Instance.ExecuteSet(cmdInsert);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// SQL query to create a connection between the order and the products in the cart.
        /// </summary>
        private void CreateConnection()
        {
            try
            {
                int commandeId;

                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT MAX(numcommande) FROM commande;"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    commandeId = Convert.ToInt32(dt.Rows[0][0]);
                }

                foreach (OrderedProduct item in Cart.Products)
                {
                    using (NpgsqlCommand cmdInsert = new NpgsqlCommand("INSERT INTO produitcommande (numcommande, numproduit, quantitecommande, prix) VALUES (@numcommande, @numproduit, @quantitecommande, @prix);"))
                    {
                        cmdInsert.Parameters.AddWithValue("@numcommande", commandeId);
                        cmdInsert.Parameters.AddWithValue("@numproduit", item.Product.Id);
                        cmdInsert.Parameters.AddWithValue("@quantitecommande", item.Quantity);
                        cmdInsert.Parameters.AddWithValue("@prix", item.Quantity * item.Product.SellingPrice);

                        DataAccess.Instance.ExecuteSet(cmdInsert);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// When the button "Purchase" is clicked, this method is called to create a new order and clear the cart.
        /// </summary>
        internal void PurchaseCart()
        {
            Debug.WriteLine(Order.Orders.Count);
            Create();
            CreateConnection();
            Cart.Products.Clear();

            Order.Orders.Clear();
            foreach (Order order in Order.FindAll())
                Order.Orders.Add(order);

            Debug.WriteLine(Order.Orders.Count);
        }
    }
}