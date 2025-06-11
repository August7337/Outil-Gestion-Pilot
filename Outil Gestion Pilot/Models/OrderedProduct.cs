using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Models
{
    public class OrderedProduct
    {
        private int quantity;
        private double price;
        private Product product;

        public OrderedProduct(int quantity, double price, Product product)
        {
            this.Quantity = quantity;
            this.Price = price;
            this.Product = product;
        }

        public int Quantity
        {
            get { return this.quantity; }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Must be greater than or equal to 0");
                this.quantity = value;
            }
        }

        public double Price
        {
            get { return this.price; }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Must be greater than or equal to 0");
                this.price = value;
            }
        }

        public Product Product
        {
            get { return this.product; }
            set { this.product = value; }
        }

        public static List<OrderedProduct> FindAll()
        {
            List<OrderedProduct> orderedProducts = new List<OrderedProduct>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT pc.quantitecommande,pc.prix,p.codeproduit,p.nomproduit,p.prixvente FROM produitcommande pc JOIN commande c ON pc.numcommande = c.numcommande JOIN produit p ON pc.numproduit = p.numproduit WHERE c.numcommande = 1;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Product product = new Product(
                        "abc", 
                        dr["codeproduit"].ToString(), 
                        dr["nomproduit"].ToString(), 
                        Convert.ToDouble(dr["prixvente"])
                    );
                    orderedProducts.Add(
                        new OrderedProduct(
                            Convert.ToInt32(dr["quantitecommande"]),
                            Convert.ToInt32(dr["prix"]),
                            product
                        )
                    );
                }
            }
            return orderedProducts;
        }
    }
}
