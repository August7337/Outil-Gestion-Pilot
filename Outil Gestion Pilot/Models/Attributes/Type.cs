using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Models.Attributes
{
    public class Type
    {
        private string name;
        private Category category;

        public Type(string name, Category category)
        {
            this.Name = name;
            this.Category = category;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Category Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

        public static List<Type> FindAll()
        {
            List<Type> type = new List<Type>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from type;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    type.Add(
                        new Type(
                            (string)dr["libelletype"],
                            Category.FindAll()[(int)dr["numcategorie"] - 1]
                        )
                    );
                }
            }

            return type;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
