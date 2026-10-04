using AnimeFigures.Model;
using System.Collections.Generic;

namespace AnimeFigures.DataAccessLayer
{
    /// <summary>
    /// Интерфейс репозитория для выполнения базовых CRUD операций
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующей IDomainObject</typeparam>
    public interface IRepository<T> where T : IDomainObject
    {
        void Add(T entity);
        void Delete(int id);
        IEnumerable<T> ReadAll();
        T ReadById(int id);
        void Update(T entity);
    }
}