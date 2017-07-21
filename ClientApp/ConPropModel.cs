using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    [DataContract]
    public class ConPropModel
    {
        [DataMember]
        public string ServerName { get; set; }
        [DataMember]
        public string DatabaseName { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }

        public ConPropModel()
        {
            ServerName = "(LocalDB)\\MSSQLLocalDB";
            DatabaseName = "testdbforuwp";
            UserName = "Pasha";
            Password = "1234";
        }

        public ConPropModel(SqlConnectionStringBuilder newConnection)
        {
            ServerName = newConnection.DataSource;
            DatabaseName = newConnection.InitialCatalog;
            UserName = newConnection.UserID;
            Password = newConnection.Password;
        }

        public ConPropModel(string newConStr)
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
