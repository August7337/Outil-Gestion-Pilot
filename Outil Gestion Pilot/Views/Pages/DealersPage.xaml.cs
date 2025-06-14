using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.ViewModels.Pages;
using Outil_Gestion_Pilot.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Controls;
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

        private void but_addRevendeur_Click(object sender, RoutedEventArgs e)
        {
            Reseller aReseller = new Reseller();
            DealerWindow window = new DealerWindow(Windows.Action.Créer, aReseller);
            if (window.ShowDialog() == true)
            {
                try
                {
                    Reseller.Resellers.Add(aReseller);
                    aReseller.Create();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Erreur");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur");
                }
                ViewModel.DealersView.Refresh();
            }
        }

        private void but_modifyRevendeur_Click(object sender, RoutedEventArgs e)
        {
            if (DealersDG.SelectedItem != null)
            {
                Reseller aReseller = (Reseller)DealersDG.SelectedItem;
                DealerWindow window = new DealerWindow(Windows.Action.Modifier, aReseller);
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