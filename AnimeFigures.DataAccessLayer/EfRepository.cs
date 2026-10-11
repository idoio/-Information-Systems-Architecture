using AnimeFigures.Model;
using Microsoft.EntityFrameworkCore;

namespace AnimeFigures.DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория с использованием Entity Framework
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующей IDomainObject</typeparam>
    public class EfRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EfRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет сущность в БД и возвращает её с заполненным Id
        /// </summary>
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Удаляет сущность по Id. Возвращает true, если удаление успешно
        /// </summary>
        public bool Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Возвращает все сущности
        /// </summary>
        public List<T> ReadAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        /// <summary>
        /// Возвращает сущность по Id или null, если не найдена
        /// </summary>
        public T? ReadById(int id)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Обновляет сущность. Возвращает true, если обновление успешно
        /// </summary>
        public bool Update(T entity)
        {
            var existing = _context.Set<T>().Find(entity.Id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return true;
        }
    }
}