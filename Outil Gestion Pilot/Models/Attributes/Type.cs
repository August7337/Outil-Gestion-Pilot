using Npgsql;
using System.Data;

namespace Outil_Gestion_Pilot.Models.Attributes
{
    public class Type
    {
        private int id;
        private string name;
        private Category category;

        public Type(int id, string name, Category category)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
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
                            (int)dr["numtype"],
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
