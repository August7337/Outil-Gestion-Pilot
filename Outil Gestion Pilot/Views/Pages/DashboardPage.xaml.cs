using Npgsql;
using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.Services;
using Outil_Gestion_Pilot.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace Outil_Gestion_Pilot.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            try
            {
                var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM employe;");
                object result = DataAccess.Instance.ExecuteSelectUneValeur(cmd);
                SqlTxt.Text = $"Connexion réussie. Nombre d'employés : {result}";
            }
            catch (Exception ex)
            {
                SqlTxt.Text = $"Erreur : {ex.Message}";
            }

            InfoTxt.Text = SessionService.Instance.Login + " est un " + SessionService.Instance.Role;
        }
    }
}
