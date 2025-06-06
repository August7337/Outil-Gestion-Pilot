

using System.Collections.Generic;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Configuration;



namespace Outil_Gestion_Pilot.Models
{

    public  class DataAccess
    {
        private static readonly DataAccess instance = new DataAccess();

        private readonly string connectionString = 
            $"Host={ConfigurationManager.AppSettings["Host"]};" +
            $"Port={ConfigurationManager.AppSettings["Port"]};" +
            $"Username={ConfigurationManager.AppSettings["Username"]};" +
            $"Password={ConfigurationManager.AppSettings["Password"]};" +
            $"Database={ConfigurationManager.AppSettings["Database"]};" +
            $"Options='-c search_path=\"{ConfigurationManager.AppSettings["Schema"]}\"'";

        private NpgsqlConnection connection;

        public static DataAccess Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Private constructor to prevent multiple instantiation
        /// </summary>
        private DataAccess()
        {
            
            try
            {
                connection = new NpgsqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de connexion GetConnection \n" + connectionString);
                throw;
            }
        }


        /// <summary>
        /// to recover the connection (and open it if necessary)
        /// </summary>
        /// <returns></returns>
        public NpgsqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, "Pb de connexion GetConnection \n" + connectionString);
                    throw;                
                }
            }

        
            return connection;
        }

        /// <summary>
        /// for SELECT queries and returns a DataTable (in-memory data table)
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable ExecuteSelect(NpgsqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection = GetConnection();
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Erreur SQL");
                throw;
            }
            return dataTable;
        }

        /// <summary>
        /// for INSERT queries and returns the generated ID
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public int ExecuteInsert(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex) { 
                LogError.Log(ex, "Pb avec une requete insert " + cmd.CommandText);
                throw; }
            return nb;
        }




        /// <summary>
        /// for UPDATE, DELETE queries
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public int ExecuteSet(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                LogError.Log(ex, "Pb avec une requete set " + cmd.CommandText);
                throw;
            }
            return nb;

        }

        /// <summary>
        /// for queries with a single return value (eg: COUNT, SUM)
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public object ExecuteSelectUneValeur(NpgsqlCommand cmd)
        {
            object res = null;
            try
            {
                cmd.Connection = GetConnection();
                res = cmd.ExecuteScalar();
            }
            catch (Exception ex) { 
                LogError.Log(ex, "Pb avec une requete select " + cmd.CommandText);
                throw;
            }
            return res;

        }

        /// <summary>
        /// Close connection
        /// </summary>
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
