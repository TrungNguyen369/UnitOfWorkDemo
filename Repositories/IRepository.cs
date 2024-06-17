// Repositories/IRepository.cs
using System.Collections.Generic;

namespace UnitOfWorkDemo.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
