using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Models.Attributes
{
    public class Category
    {
        public static List<Category> Categorys = Category.FindAll();
        private int id;
        private string name;

        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public static List<Category> FindAll()
        {
            List<Category> categorys = new List<Category>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from categorie;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    categorys.Add(
                        new Category(
                            (int)dr["numcategorie"],
                            (string)dr["libellecategorie"]
                        )
                    );
                }
            }

            return categorys;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
