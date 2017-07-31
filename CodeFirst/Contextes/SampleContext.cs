using System.Configuration;
using System.Data.Entity;
using CodeFirst.Entities;

namespace CodeFirst.Contextes
{
    public class SampleContext : DbContext
    {
        internal SampleContext(string conStr): base (conStr)
        {
            Database.SetInitializer<SampleContext>(new DBInitializer());
        }

        public DbSet<HistoryRow> HistoryRows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoryRow>().HasKey(p => new {p.EventTime, p.Type, p.DeviceSerialNumber });
            modelBuilder.Entity<HistoryRow>().Property(p => p.EventTime).HasColumnType("datetime2");
            base.OnModelCreating(modelBuilder);
        }
    }
}
