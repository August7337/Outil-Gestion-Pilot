using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Npgsql;
using System.Data;
using Outil_Gestion_Pilot.Views.Pages;


namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public partial class OrderVisualisationViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<OrderedProduct> orderedProducts;

        public ICollectionView OrderedProductView { get; set; }

        public OrderVisualisationViewModel ViewModel
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public OrderVisualisationViewModel(Order uneCommande)
        {
            int idCommande = uneCommande.CommandeId;
            OrderedProducts = new ObservableCollection<OrderedProduct>(OrderedProduct.FindAll(idCommande));
            OrderedProductView = CollectionViewSource.GetDefaultView(OrderedProducts);
        }

        /// <summary>
        /// Delete the order from the database and refresh the view.
        /// </summary>
        /// <param name="uneCommande"></param>
        public void butSupprimer_Click(Order uneCommande)
        {
            MessageBoxResult result = MessageBox.Show("Voulez-vous supprimer cette commade ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int idCommande = uneCommande.CommandeId;

                using (NpgsqlCommand cmdselect = new NpgsqlCommand("DELETE FROM PRODUITCOMMANDE WHERE numcommande = " + idCommande + ";")) //SQl Query for delete the order 
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdselect);
                }
                MessageBox.Show("Suppression effectué.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                CollectionViewSource.GetDefaultView(OrderedProducts).Refresh();

            }
            else
            {
                MessageBox.Show("Suppression annulée.", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Calculate the total price of the order including all products.
        /// </summary>
        /// <returns></returns>
        public double ResolvePriceTTC()
        {
            double price = 0;
            foreach (OrderedProduct aproduct in OrderedProducts)
            {
                price += aproduct.Price;
            }
            return price;
        }

        /// <summary>
        /// Navigate back to the OrdersPage.
        /// </summary>
        internal void butRetour_Click()
        {
            OrdersViewModel ordersViewModel = new OrdersViewModel(); // Create a new instance of OrdersViewModel
            OrdersPage page = new OrdersPage(ordersViewModel);
        }
    }
}