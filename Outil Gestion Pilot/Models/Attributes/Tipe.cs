using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Models.Attributes
{
    public class Tipe
    {
        private int id;
        private string name;

        public static List<Tipe> Tipes = Tipe.FindAll();

        public Tipe(int id, string name)
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

        public static List<Tipe> FindAll()
        {
            List<Tipe> tipes = new List<Tipe>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from typepointe;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    tipes.Add(
                        new Tipe(
                            (int)dr["numtypepointe"],
                            (string)dr["libelletypepointe"]
                        )
                    );
                }
            }

            return tipes;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
