using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Outil_Gestion_Pilot.Models
{
    public enum ProductCategory { Bureau, Loisir }
    public enum ProductType { Bille, Roller_gel, Roller_liquide, Plume, Feutre }
    public enum ProductTipe { Fine, Moyenne, Epaisse}
    public enum ProductColor { Bleu, Vert, Rouge, Noire }

    public class Product
    {
        private string imagePath;
        private string code;
        private string name;
        private ProductCategory category;
        private ProductType type;
        private ProductTipe tipe;
        private double sellingPrice;
        private int stock;
        private List<ProductColor> color;
        private int desiredQuantity;

        public Product()
        {
        }

        public Product(string imagePath, string code, string name, ProductCategory category, ProductType type, ProductTipe tipe, double sellingPrice, int stock, List<ProductColor> color)
        {
            this.ImagePath = imagePath;
            this.Code = code;
            this.Name = name;
            this.Category = category;
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

        public ProductCategory Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

        public ProductType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public ProductTipe Tipe
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

        public List<ProductColor> Color
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
                    ProductType type = new ProductType();
                    ProductTipe tipe = new ProductTipe();

                    products.Add(
                        new Product(
                            "à implémenter", // To do
                            (string)dr["codeproduit"],
                            (string)dr["nomproduit"],
                            ProductCategory.Bureau, // To do
                            (ProductType)((int)dr["numtype"] - 1), // To modify
                            (ProductTipe)((int)dr["numtypepointe"] - 1), // To modify
                            Convert.ToDouble(dr["prixvente"]), 
                            (int)dr["quantitestock"],
                            new List<ProductColor>() // To do
                        )
                    );
                }
            }
            return products;
        }
    }
}