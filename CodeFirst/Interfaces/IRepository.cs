using System;
using System.Collections.Generic;

namespace CodeFirst.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetItems(); // получение всех объектов
        //T GetItem(Guid id); // получение одного объекта по id
        T GetItem(params object[] keyObjects); // получение одного объекта
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        //void Delete(Guid id); // удаление объекта по id
        void Delete(params object[] keyObjects); // удаление объекта
        void Save();  // сохранение изменений
    }
}