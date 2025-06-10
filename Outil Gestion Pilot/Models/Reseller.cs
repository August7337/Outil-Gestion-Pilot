using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Models
{
    public class Reseller
    {
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
    }
}
