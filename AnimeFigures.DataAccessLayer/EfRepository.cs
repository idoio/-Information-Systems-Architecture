using AnimeFigures.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AnimeFigures.DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория с использованием Entity Framework
    /// </summary>
    public class EfRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly AppDbContext _context;

        public EfRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> ReadAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T ReadById(int id)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}