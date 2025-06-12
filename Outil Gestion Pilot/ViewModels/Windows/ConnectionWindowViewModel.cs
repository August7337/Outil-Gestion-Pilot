using Npgsql;
using Outil_Gestion_Pilot.Models;
using Outil_Gestion_Pilot.Services;
using Outil_Gestion_Pilot.Views.Windows;
using System.Windows.Controls;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.ViewModels.Windows
{
    public partial class ConnectionWindowViewModel : ObservableObject
    {
        private readonly SessionService sessionService;

        public ConnectionWindowViewModel(SessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        /// <summary>
        /// Attempts to authenticate a user by verifying the credentials against the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>
        /// The username if authentication is successful;
        /// otherwise, an error or informational message indicating the failure.
        /// </returns>
        public string Connection(string username, string password)
        {
            string message;

            try
            {
                var cmd = new NpgsqlCommand("SELECT login FROM employe WHERE login = @username AND password = @password");
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                
                object result = DataAccess.Instance.ExecuteSelectUneValeur(cmd);
                if (result != null)
                {
                    sessionService.Login = result.ToString();
                    return result.ToString();
                }
                else message = "Informations incorrects";
            }
            catch (Exception ex)
            {
                message = $"Erreur : {ex.Message}";
            }

            return message;
        }
    }
}
