using AnimeFigures.Model;

namespace AnimeFigures.DataAccessLayer
{
    /// <summary>
    /// Интерфейс репозитория для выполнения базовых CRUD операций
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующей IDomainObject</typeparam>
    public interface IRepository<T> where T : IDomainObject
    {
        /// <summary>
        /// Добавляет новую сущность. Возвращает добавленную сущность с заполненным Id.
        /// </summary>
        T Add(T entity);

        /// <summary>
        /// Удаляет сущность по Id. Возвращает true, если удаление успешно.
        /// </summary>
        bool Delete(int id);

        /// <summary>
        /// Возвращает все сущности.
        /// </summary>
        List<T> ReadAll();

        /// <summary>
        /// Возвращает сущность по Id или null, если не найдена.
        /// </summary>
        T? ReadById(int id);

        /// <summary>
        /// Обновляет сущность. Возвращает true, если обновление успешно.
        /// </summary>
        bool Update(T entity);
    }
}