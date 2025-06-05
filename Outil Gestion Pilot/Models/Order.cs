using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Models
{
    public class Order
    {
        private string reseller;
        private DateTime orderDate;

        public string Reseller
        {
            get{ return this.reseller;}
            set{ this.reseller = value;}
        }

        public DateTime OrderDate
        {
            get{return this.orderDate;}
            set{this.orderDate = value;}
        }
    }
}
