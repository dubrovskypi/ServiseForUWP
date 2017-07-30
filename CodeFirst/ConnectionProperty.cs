using System.Data.SqlClient;

namespace CodeFirst
{
    public class ConnectionProperty
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool TrustServerCertificate { get; private set; }
        public bool IntegratedSecurity { get; private set; }

        public ConnectionProperty()
        {
            ServerName = "(LocalDB)\\MSSQLLocalDB";
            DatabaseName = "testdbforuwp";
            UserName = "Pasha";
            Password = "1234";
            IntegratedSecurity = true;
            TrustServerCertificate = true;
        }

        public ConnectionProperty(SqlConnectionStringBuilder newConnection)
        {
            ServerName = newConnection.DataSource;
            DatabaseName = newConnection.InitialCatalog;
            UserName = newConnection.UserID;
            Password = newConnection.Password;
            IntegratedSecurity = newConnection.IntegratedSecurity;
            TrustServerCertificate = newConnection.TrustServerCertificate;
        }

        public ConnectionProperty(string newConStr)
        {
            var newConnection = new SqlConnectionStringBuilder(newConStr);
            ServerName = newConnection.DataSource;
            DatabaseName = newConnection.InitialCatalog;
            UserName = newConnection.UserID;
            Password = newConnection.Password;
            TrustServerCertificate = newConnection.TrustServerCertificate;
            IntegratedSecurity = newConnection.IntegratedSecurity;
        }

        public string GetConnectionString()
        {
            var constr = new SqlConnectionStringBuilder()
            {
                DataSource = ServerName,
                InitialCatalog = DatabaseName,
                UserID = UserName,
                Password = Password,
                IntegratedSecurity = IntegratedSecurity,
                TrustServerCertificate = TrustServerCertificate
            };
            return constr.ConnectionString;
        }
    }
}
