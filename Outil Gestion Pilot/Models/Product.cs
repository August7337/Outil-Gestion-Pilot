using System;
using System.Collections.Generic;
using System.Linq;

namespace Outil_Gestion_Pilot.Models
{
    public enum ProductCategory { Bureau, Loisir }
    public enum ProductType { Bille, Roller_gel, Couleur_fun, Frixion_Ball }
    public enum ProductTipe { Fine, Moyenne, Grosse }
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
    }
}