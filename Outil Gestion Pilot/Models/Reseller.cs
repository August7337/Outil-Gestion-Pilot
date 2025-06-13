using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Models
{
    public class Reseller
    {
        public static List<Reseller> resellers = new List<Reseller>(); 


        private int numeroRevendeur;
        private string raisonSociale;
        private string rue;
        private string cp;
        private string ville;

        public Reseller()
        {
        }

        public Reseller(int numeroRevendeur, string raisonSociale, string rue, string cp, string ville)
        {
            this.NumeroRevendeur = numeroRevendeur;
            this.RaisonSociale = raisonSociale;
            this.Rue = rue;
            this.Cp = cp;
            this.Ville = ville;
        }

        public int NumeroRevendeur
        {
            get
            {
                return this.numeroRevendeur;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Le numéro de revendeur ne peut pas être négatif");
                }
                this.numeroRevendeur = value;
            }
        }

        public string RaisonSociale
        {
            get
            {
                return this.raisonSociale;
            }

            set
            {
                this.raisonSociale = value;
            }
        }

        public string Rue
        {
            get
            {
                return this.rue;
            }

            set
            {
                this.rue = value;
            }
        }

        public string Cp
        {
            get
            {
                return this.cp;
            }

            set
            {
                if (value.Length != 5)
                {
                    throw new ArgumentException("Erreur sur le format du code postal");
                }
                this.cp = value;
            }
        }

        public string Ville
        {
            get
            {
                return this.ville;
            }

            set
            {
                this.ville = value;
            }
        }

        public  List<Reseller> FindAll()
        {
            List<Reseller> resellers = new List<Reseller>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM revendeur ORDER BY numrevendeur;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    resellers.Add(new Reseller((int)dr["numRevendeur"], (string)dr["raisonSociale"], (string)dr["adresserue"], (string)dr["adressecp"], (string)dr["adresseville"]));
            }
            return resellers;
        }

        public void Create()
        {
            using (var cmdInsert = new NpgsqlCommand("INSERT INTO REVENDEUR (numrevendeur, raisonsociale, adresserue, adressecp, adresseville) values (@numrevendeur, @raisonsociale, @adresserue, @adressecp, @adresseville)"))
            {
                cmdInsert.Parameters.AddWithValue("numrevendeur", this.NumeroRevendeur);
                cmdInsert.Parameters.AddWithValue("raisonsociale", this.RaisonSociale);
                cmdInsert.Parameters.AddWithValue("adresserue", this.Rue);
                cmdInsert.Parameters.AddWithValue("adressecp", this.Cp);
                cmdInsert.Parameters.AddWithValue("adresseville", this.Ville);
                DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
        }

        public void Update()
        {
            using (var cmdInsert = new NpgsqlCommand("UPDATE Revendeur SET raisonsociale=@raisonsociale, adresserue=@adresserue, adressecp=@adressecp, adresseville=@adresseville WHERE numrevendeur = @numrevendeur"))
            {
                cmdInsert.Parameters.AddWithValue("numrevendeur", this.NumeroRevendeur);
                cmdInsert.Parameters.AddWithValue("raisonsociale", this.RaisonSociale);
                cmdInsert.Parameters.AddWithValue("adresserue", this.Rue);
                cmdInsert.Parameters.AddWithValue("adressecp", this.Cp);
                cmdInsert.Parameters.AddWithValue("adresseville", this.Ville);
                DataAccess.Instance.ExecuteSet(cmdInsert);
            }
        }

        
    }
}
