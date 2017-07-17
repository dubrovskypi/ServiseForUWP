using System;
using System.Collections.Generic;
using System.Data.Entity;
using CodeFirst.Contextes;
using CodeFirst.Entities;
using CodeFirst.Interfaces;

namespace CodeFirst.Repositories
{
    public class CustomRepository : IRepository<Customer>
    {
        private SampleContext _db;

        internal CustomRepository()
        {
            this._db = new SampleContext();
            //InitDB();
        }

        internal CustomRepository(SampleContext ctx)
        {
            this._db = ctx;
            //InitDB();
        }

        //переделать
        //private void InitDB()
        //{
        //    try
        //    {
        //        var init = new DBInitializer();
        //        init.InitializeDatabase(this._db);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}

        public IEnumerable<Customer> GetItems()
        {
            return _db.Customers;
        }

        public Customer GetItem(int id)
        {
            return _db.Customers.Find(id);
        }

        public void Create(Customer customer)
        {
            _db.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _db.Entry(customer).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Customer customer = _db.Customers.Find(id);
            if (customer != null)
                _db.Customers.Remove(customer);
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
