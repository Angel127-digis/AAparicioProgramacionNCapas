using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Connection
    {
        public static string GetConnection()
        {
            String connection = ConfigurationManager.ConnectionStrings["AAparicioProgramacionNCapas"].ToString();
            return connection;
        }
    }
}
