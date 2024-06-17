// Repositories/UnitOfWork.cs
using UnitOfWorkDemo.Data;
using UnitOfWorkDemo.Models;

namespace UnitOfWorkDemo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Repository<Product> _productRepository;
        private Repository<Order> _orderRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Product> Products => _productRepository ??= new Repository<Product>(_context);
        public IRepository<Order> Orders => _orderRepository ??= new Repository<Order>(_context);


        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
