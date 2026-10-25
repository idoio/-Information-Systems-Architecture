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
        /// <param name="entity">Сущность для добавления</param>
        /// <returns>Добавленная сущность с заполненным идентификатором</returns>
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Удаляет сущность по Id
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности</param>
        /// <returns>True, если удаление успешно; иначе False</returns>
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
        /// Возвращает список всех сущностей
        /// </summary>
        /// <returns>Список сущностей типа T</returns>
        public List<T> ReadAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        /// <summary>
        /// Возвращает сущность по Id
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности</param>
        /// <returns>Объект сущности или null, если не найдена</returns>
        public T? ReadById(int id)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Обновляет данные сущности
        /// </summary>
        /// <param name="entity">Сущность с обновленными данными</param>
        /// <returns>True, если обновление успешно; иначе False</returns>
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