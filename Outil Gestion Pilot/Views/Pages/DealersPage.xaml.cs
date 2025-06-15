using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using Outil_Gestion_Pilot.Views.Windows;
using MessageBox = System.Windows.MessageBox;


namespace Outil_Gestion_Pilot.Views.Pages
{
    public partial class DealersPage
    {
        public DealersViewModel ViewModel { get; }

        public DealersPage(DealersViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
        }

        public DealersPage()
        {
        }

        /// <summary>
        /// Open the DealerWindow to add a new dealer and refresh the view if the dealer is added successfully.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_addRevendeur_Click(object sender, RoutedEventArgs e)
        {
            Reseller aReseller = new Reseller();
            DealerWindow window = new DealerWindow(Windows.Action.Créer, aReseller);
            if (window.ShowDialog() == true)
            {
                try
                {
                    Reseller.resellers.Add(aReseller);
                    aReseller.Create(); 
                    ViewModel.DealersView.Refresh();
                    DealersViewModel dealersViewModel = new DealersViewModel(); // to update the datagrid, we create a new DealerViewModel instance
                    DealersPage page = new DealersPage(dealersViewModel);
                    NavigationService.Navigate(page);

                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Erreur");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur");
                }
            }
        }

        /// <summary>
        /// Open the DealerWindow to modify the selected dealer and refresh the view if the dealer is modified successfully.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_modifyRevendeur_Click(object sender, RoutedEventArgs e)
        {
            if (DealersDG.SelectedItem != null) //we can modify a dealer if one of them is selected 
            {
                Reseller aReseller = (Reseller)DealersDG.SelectedItem;
                DealerWindow window = new DealerWindow(Windows.Action.Modifier, aReseller); // It's use the same Window as Add a Dealer 
                if (window.ShowDialog() == true)
                {
                    try
                    {
                        aReseller.Update();
                    }
                    catch (Exception ex)
                    {
                    }
                    ViewModel.DealersView.Refresh();

                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un revendeur à modifier.", "Aucun revendeur sélectionné", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}