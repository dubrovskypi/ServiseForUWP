using System.Configuration;
using System.Data.Entity;
using CodeFirst.Entities;

namespace CodeFirst.Contextes
{
    public class SampleContext : DbContext
    {
        internal SampleContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        //public SampleContext() : base()
        {
            //Database.SetInitializer<SampleContext>(new DBInitializer());
            //var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //Database.Connection.ConnectionString = cs;
        }

        internal SampleContext(string conStr): base (conStr)
        {

        }
        //static SampleContext()
        //{
        //    //Database.SetInitializer(new DBInitializer());
        //}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<HistoryRow> HistoryRows { get; set; }
    }
}
