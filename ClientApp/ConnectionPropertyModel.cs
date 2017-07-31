using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class ConnectionPropertyModel
    {
        public Guid Id { get; set; }
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ConnectionPropertyModel()
        {
            ConnectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=RemoteDb;Integrated Security=True;TrustServerCertificate=True";
            Id = Guid.NewGuid();
            UserName = "Pasha";
            Password = "1234";
        }

        public ConnectionPropertyModel(string conStr, Guid id, string username, string password)
        {
            ConnectionString = conStr;
            Id = id;
            UserName = username;
            Password = password;
        }
    }
}
