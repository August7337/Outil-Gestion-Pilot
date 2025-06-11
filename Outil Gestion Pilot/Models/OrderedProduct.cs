using System;
using System.Collections.Generic;
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
    }
}
