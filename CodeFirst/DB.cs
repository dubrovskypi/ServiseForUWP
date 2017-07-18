using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirst.Contextes;
using CodeFirst.Entities;
using CodeFirst.Interfaces;
using CodeFirst.Repositories;

namespace CodeFirst
{
    public static class DB
    {
        private static SqlConnectionStringBuilder _connStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "(LocalDB)\\MSSQLLocalDB", //server
            InitialCatalog = "testdbforuwp", //DB name
            //IntegratedSecurity = true,

            //IntegratedSecurity = false,
            //Pooling = false,
            //PersistSecurityInfo = false,
            //MultipleActiveResultSets = true,

            //ApplicationName = "EntityFramework",
            UserID = "Pasha",
            Password = "1234",
            //Password = "POLIapplehouse93",

        };

        public static string ConnectionString
        {
            get { return _connStringBuilder.ConnectionString; }
            set { _connStringBuilder = new SqlConnectionStringBuilder(value); }
        }

        private static SqlConnection _connection;
        public static SqlConnection Connection
        {
            get
            {
                return _connection ?? (_connection = new SqlConnection()
                {
                    ConnectionString = ConnectionString
                });
            }
        }

        //выдает исключение если нельзя открыть соединение
        public static void TestConnection(string connection)
        {
            Connection.ConnectionString = connection;
            Connection.Open();
            Connection.Close();
            //var b = Database.Exists(ConnectionString);
        }

        public static bool DatabaseExists(string connection)
        {
            return Database.Exists(connection);
        }

        public static void CreateDatabase()
        {
            try
            {
                using (var context = new SampleContext(ConnectionString))
                {
                    var init = new DBInitializer();
                    init.InitializeDatabase(context);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        //public static CustomRepository CreateRepository()
        //{
        //    return new CustomRepository(new SampleContext(ConnectionString));
        //}
        public static CustomRepository CreateCustomRepository()
        {
            return new CustomRepository(new SampleContext(ConnectionString));
        }

        public static HistoryRepository CreateHistoryRepository()
        {
            return new HistoryRepository(new SampleContext(ConnectionString));
        }


    }
}
