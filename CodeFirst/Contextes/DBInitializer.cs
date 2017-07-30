using System;
using System.Data.Entity;
using CodeFirst.Entities;

namespace CodeFirst.Contextes
{
    internal class DBInitializer: DropCreateDatabaseIfModelChanges<SampleContext>
    {
        protected override void Seed(SampleContext context)
        {
            var defHistoryRow = new HistoryRow
            {
                //HistoryRowId = Guid.NewGuid(),
                //Id = Guid.NewGuid(),
                Cps = 0.1,
                De = 0.2,
                Der = 0.3,
                EventTime = DateTime.Now,
                Type = HistoryType.DeviceOn,
                DeviceSerialNumber = Guid.NewGuid().ToString(),
                ReaderSerialNumber = "serialreader",
                IsSynchronized = false
            };
            context.HistoryRows.Add(defHistoryRow);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
