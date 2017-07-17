using System;
using System.Data.Entity;
using CodeFirst.Entities;

namespace CodeFirst.Contextes
{
    internal class DBInitializer: DropCreateDatabaseAlways<SampleContext>
    {
        protected override void Seed(SampleContext context)
        {
            var defCustomer = new Customer
            {
                Age = 99,
                CustomerId = Guid.NewGuid(),
                Email = "def@mail.com",
                Name = "DefName",
                Orders = null,
                Photo = null
            };
            context.Customers.Add(defCustomer);
            //context.SaveChanges();

            var defHistoryRow = new HistoryRow
            {
                HistoryRowId = Guid.NewGuid(),
                Cps = 1,
                De = 2,
                Der = 3,
                Time = DateTime.Now,
                Type = 0
            };
            context.HistoryRows.Add(defHistoryRow);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
