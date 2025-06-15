using Outil_Gestion_Pilot.Models;

namespace Outil_Gestion_Pilot.ViewModels.Pages
{
    public partial class ProductVisualisationViewModel : ObservableObject
    {
        private Product product;
        [ObservableProperty]
        private string disponibilityButtonText;

        public ProductVisualisationViewModel(Product product)
        {
            Product = product;
            DisponibilityButtonText = product.Disponibility ? "Rendre Indisponible" : "Rendre Disponible";
        }

        public Product Product
        {
            get { return this.product; }
            set 
            { 
                this.product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        [RelayCommand]
        private void ToggleDisponibility()
        {
            if (Product.Disponibility == true)
            {
                Product.Disponibility = false;
                DisponibilityButtonText = "Rendre Disponible";
                Product.Update();
            }
            else
            {
                Product.Disponibility = true;
                DisponibilityButtonText = "Rendre Indisponible";
                Product.Update();
            }
        }
    }
}
