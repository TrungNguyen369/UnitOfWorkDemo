// Repositories/IUnitOfWork.cs
using System;
using UnitOfWorkDemo.Models;

namespace UnitOfWorkDemo.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        void Complete();
    }
}
