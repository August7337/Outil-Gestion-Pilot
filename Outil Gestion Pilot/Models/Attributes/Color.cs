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
        private string name;

        public static List<Color> Colors = Color.FindAll();
        public Color(string name)
        {
            this.Name = name;
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
