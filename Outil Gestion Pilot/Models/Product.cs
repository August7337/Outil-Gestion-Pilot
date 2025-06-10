using Npgsql;
using Outil_Gestion_Pilot.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Outil_Gestion_Pilot.Models
{
    public class Product
    {
        private string imagePath;
        private string code;
        private string name;
        private Attributes.Type type;
        private Tipe tipe;
        private double sellingPrice;
        private int stock;
        private List<Color> color;
        private int desiredQuantity;

        public Product()
        {
        }

        public Product(string imagePath, string code, string name, Attributes.Type type, Tipe tipe, double sellingPrice, int stock, List<Color> color)
        {
            this.ImagePath = imagePath;
            this.Code = code;
            this.Name = name;
            this.Type = type;
            this.Tipe = tipe;
            this.SellingPrice = sellingPrice;
            this.Stock = stock;
            this.Color = color;
        }

        public string ImagePath
        {
            get { return this.imagePath; }
            set { this.imagePath = value; }
        }

        public Category Category
        {
            get { return this.type.Category; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Attributes.Type Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public Tipe Tipe
        {
            get { return this.tipe; }
            set { this.tipe = value; }
        }

        public double SellingPrice
        {
            get { return this.sellingPrice; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("The sale price value must be greater than or equal to 0");
                this.sellingPrice = value;
            }
        }

        public int Stock
        {
            get { return this.stock; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("The stock value must be greater than or equal to 0");
                this.stock = value;
            }
        }

        public List<Color> Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public string ColorDisplay
        {
            get
            {
                if (color == null || !color.Any())
                    return "Aucune";

                return string.Join(", ", color.Select(c => c.ToString()));
            }
        }

        public int DesiredQuantity
        {
            get
            {
                return this.desiredQuantity;
            }

            set
            {
                this.desiredQuantity = value;
            }
        }

        public List<Product> FindAll()
        {
            List<Product> products = new List<Product>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from produit;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    List<Color> colors = FindColors((string)dr["codeproduit"]);

                    products.Add(
                        new Product(
                            "à implémenter",
                            (string)dr["codeproduit"],
                            (string)dr["nomproduit"],
                            Attributes.Type.FindAll()[(int)dr["numtype"] - 1],
                            Tipe.FindAll()[(int)dr["numtypepointe"] - 1],
                            Convert.ToDouble(dr["prixvente"]), 
                            (int)dr["quantitestock"],
                            FindColors((string)dr["codeproduit"])
                        )
                    );
                }
            }
            return products;
        }

        private List<Color> FindColors(string v)
        {
            List<Color> colors = new List<Color>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT c.libellecouleur\r\nFROM produit p\r\nJOIN couleurproduit cp ON p.numproduit = cp.numproduit\r\nJOIN couleur c ON cp.numcouleur = c.numcouleur\r\nWHERE p.codeproduit = '" + v + "';"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Color findedColor = Attributes.Color.Colors.Find(c => c.Name == dr["libellecouleur"].ToString());
                    colors.Add(findedColor);
                }
            }
            return colors;
        }
    }
}