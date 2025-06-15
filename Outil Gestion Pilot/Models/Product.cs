using Npgsql;
using Outil_Gestion_Pilot.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace Outil_Gestion_Pilot.Models
{
    public partial class Product : ObservableObject
    {
        public static List<Product> Products = Product.FindAll();

        private int id;
        private string imagePath;
        private string code;
        private string name;
        private Attributes.Type type;
        private Tipe tipe;
        private double sellingPrice;
        private int stock;
        private List<Color> color;
        private int desiredQuantity;

        [ObservableProperty]
        private bool disponibility;

        public Product()
        {
        }

        public Product(string imagePath, string code, string name, double sellingPrice)
        {
            this.ImagePath = imagePath;
            this.Code = code;
            this.Name = name;
            this.SellingPrice = sellingPrice;
        }

        public Product(int id, string imagePath, string code, string name, Attributes.Type type, Tipe tipe, double sellingPrice, int stock, List<Color> color, bool disponibility)
        {
            this.Id = id;
            this.ImagePath = imagePath;
            this.Code = code;
            this.Name = name;
            this.Type = type;
            this.Tipe = tipe;
            this.SellingPrice = sellingPrice;
            this.Stock = stock;
            this.Color = color;
            this.Disponibility = disponibility;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
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

        public static List<Product> FindAll()
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
                            (int)dr["numproduit"],
                            "à implémenter",
                            (string)dr["codeproduit"],
                            (string)dr["nomproduit"],
                            Attributes.Type.FindAll()[(int)dr["numtype"] - 1],
                            Tipe.FindAll()[(int)dr["numtypepointe"] - 1],
                            Convert.ToDouble(dr["prixvente"]), 
                            (int)dr["quantitestock"],
                            FindColors((string)dr["codeproduit"]),
                            (bool)dr["disponible"]
                        )
                    );
                }
            }
            return products;
        }

        public void Create()
        {
            try
            {
                using (NpgsqlCommand cmdInsert = new NpgsqlCommand(
                    "INSERT INTO produit (numtypepointe, numtype, codeproduit, nomproduit, prixvente, quantitestock, disponible) " +
                    "VALUES (@numtypepointe, @numtype, @codeproduit, @nomproduit, @prixvente, @quantitestock, @disponible);"))
                {
                    cmdInsert.Parameters.AddWithValue("@numtypepointe", this.Tipe.Id);
                    cmdInsert.Parameters.AddWithValue("@numtype", this.Type.Id);
                    cmdInsert.Parameters.AddWithValue("@codeproduit", this.Code);
                    cmdInsert.Parameters.AddWithValue("@nomproduit", this.Name);
                    cmdInsert.Parameters.AddWithValue("@prixvente", this.SellingPrice);
                    cmdInsert.Parameters.AddWithValue("@quantitestock", this.Stock);
                    cmdInsert.Parameters.AddWithValue("@disponible", this.Disponibility);

                    DataAccess.Instance.ExecuteSet(cmdInsert);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update produit set codeproduit =@code, " +
                "nomproduit = @name,  " +
                "numtype = @type,  " +
                "numtypepointe = @tipe, " +
                "prixvente = @price, " +
                "quantitestock = @stock, " +
                "disponible = @disponibility " +
                "where numproduit = @id;"))
            {
                cmdUpdate.Parameters.AddWithValue("code", this.Code);
                cmdUpdate.Parameters.AddWithValue("name", this.Name);
                cmdUpdate.Parameters.AddWithValue("type", this.Type.Id);
                cmdUpdate.Parameters.AddWithValue("tipe", this.Tipe.Id);
                cmdUpdate.Parameters.AddWithValue("price", this.SellingPrice);
                cmdUpdate.Parameters.AddWithValue("stock", this.Stock);
                cmdUpdate.Parameters.AddWithValue("disponibility", this.Disponibility);
                cmdUpdate.Parameters.AddWithValue("id", this.Id);

                int affectedRows = DataAccess.Instance.ExecuteSet(cmdUpdate);

                Products = Product.FindAll();

                return affectedRows;
            }
        }

        private static List<Color> FindColors(string v)
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