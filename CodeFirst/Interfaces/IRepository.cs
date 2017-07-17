using System;
using System.Collections.Generic;

namespace CodeFirst.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetItems(); // получение всех объектов
        T GetItem(int id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}