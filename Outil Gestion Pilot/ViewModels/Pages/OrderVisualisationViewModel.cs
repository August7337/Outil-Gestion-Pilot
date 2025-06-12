using Outil_Gestion_Pilot.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Npgsql;
using System.Data;


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

        public void butSupprimer_Click(Order uneCommande)
        {

            MessageBoxResult result = MessageBox.Show("Voulez-vous supprimer cette commade ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int idCommande = uneCommande.CommandeId;

                using (NpgsqlCommand cmdselect = new NpgsqlCommand("DELETE FROM PRODUITCOMMANDE WHERE numcommande = " + idCommande + ";"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdselect);
                }
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("DELETE FROM COMMANDE WHERE numcommande = " + idCommande + ";"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                }
                MessageBox.Show("Suppression effectué.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            else
            {
                MessageBox.Show("Suppression annulée.", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public double ResolvePriceTTC()
        {
            double price = 0;
            foreach (OrderedProduct aproduct in OrderedProducts)
            {
                price += aproduct.Price;
            }
            return price;
        }
    }
}