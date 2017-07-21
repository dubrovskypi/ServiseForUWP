using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    [DataContract]
    public class ConnectionProperty
    {
        [DataMember]
        public string ServerName { get; set; }
        [DataMember]
        public string DatabaseName { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }

        public ConnectionProperty()
        {
            ServerName = "(LocalDB)\\MSSQLLocalDB";
            DatabaseName = "testdbforuwp";
            UserName = "Pasha";
            Password = "1234";
        }

        public ConnectionProperty(SqlConnectionStringBuilder newConnection)
        {
            ServerName = newConnection.DataSource;
            DatabaseName = newConnection.InitialCatalog;
            UserName = newConnection.UserID;
            Password = newConnection.Password;
        }

        public ConnectionProperty(string newConStr)
        {
            var newConnection = new SqlConnectionStringBuilder(newConStr);
            ServerName = newConnection.DataSource;
            DatabaseName = newConnection.InitialCatalog;
            UserName = newConnection.UserID;
            Password = newConnection.Password;
        }

        public string GetConnectionString()
        {
            var constr = new SqlConnectionStringBuilder()
            {
                DataSource = ServerName,
                InitialCatalog = DatabaseName,
                UserID = UserName,
                Password = Password
            };
            return constr.ConnectionString;
        }
    }
}
