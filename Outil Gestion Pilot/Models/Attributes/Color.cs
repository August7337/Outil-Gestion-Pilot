using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Models.Attributes
{
    public class Color
    {
        private int id;
        private string name;

        public static List<Color> Colors = Color.FindAll();

        public Color(int id, string name)
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

        public static List<Color> FindAll()
        {
            List<Color> colors = new List<Color>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from couleur;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    colors.Add(
                        new Color(
                            (int)dr["numcouleur"],
                            (string)dr["libellecouleur"]
                        )
                    );
                }
            }

            return colors;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
