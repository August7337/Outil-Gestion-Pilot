using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outil_Gestion_Pilot.Services
{
    public class SessionService
    {
        private static SessionService instance = new SessionService();

        public static SessionService Instance => instance;

        public string Login { get; set; }
        public string Role { get; set; }

        private SessionService() { }
    }
}
