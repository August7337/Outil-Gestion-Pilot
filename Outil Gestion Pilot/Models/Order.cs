using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Outil_Gestion_Pilot.Models
{
    public class Order
    {
        private string reseller;
        private DateTime orderDate;
        private string delivery;
        private List<Product> products;

        public Order()
        {
        }

        public Order(string reseller, DateTime orderDate, string delivery)
        {
            this.Reseller = reseller;
            this.OrderDate = orderDate;
            this.Delivery = delivery;
            Products = new List<Product>();
        }

        public string Reseller
        {
            get { return this.reseller; }
            set { this.reseller = value; }
        }

        public DateTime OrderDate
        {
            get { return this.orderDate; }
            set { this.orderDate = value; }
        }

        public string Delivery
        {
            get { return this.delivery; }

            set { this.delivery = value; }
        }

        public List<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public List<Order> FindAll()
        {
            List<Order> orders = new List<Order>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT r.raisonsociale,c.datecommande,m.libelletransport  FROM commande c JOIN modetransport m ON c.numtransport = m.numtransport JOIN revendeur r ON c.numrevendeur = r.numrevendeur;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {

                    orders.Add(
                        new Order(
                            (string)dr["raisonsociale"],
                            Convert.ToDateTime(dr["datecommande"]),
                            (string)dr["libelletransport"]
                        )
                    );
                }
            }
            return orders;
        }
    }
}
