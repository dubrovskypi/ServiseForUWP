using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirst.Contextes;
using CodeFirst.Entities;
using CodeFirst.Interfaces;

namespace CodeFirst.Repositories
{
    public class HistoryRepository : IRepository<HistoryRow>
    {
        private SampleContext _db;

        internal HistoryRepository(SampleContext ctx)
        {
            this._db = ctx;
        }

        public IEnumerable<HistoryRow> GetItems()
        {
            _db.HistoryRows.Load();
            return _db.HistoryRows;
        }

        //public HistoryRow GetItem(Guid id)
        //{
        //    return _db.HistoryRows.Find(id);
        //}

        public HistoryRow GetItem(params object[] keyObjects)
        {
            return _db.HistoryRows.Find(keyObjects);
        }

        public void Create(HistoryRow historyrow)
        {
            _db.HistoryRows.Add(historyrow);
        }

        public void Update(HistoryRow historyrow)
        {
            _db.Entry(historyrow).State = EntityState.Modified;
        }

        //public void Delete(Guid id)
        //{
        //    HistoryRow historyrow = _db.HistoryRows.Find(id);
        //    if (historyrow != null)
        //        _db.HistoryRows.Remove(historyrow);
        //}

        public void Delete(params object[] keyObjects)
        {
            HistoryRow historyrow = _db.HistoryRows.Find(keyObjects);
            if (historyrow != null)
                _db.HistoryRows.Remove(historyrow);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
