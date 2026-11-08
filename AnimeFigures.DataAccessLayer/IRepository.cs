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
        /// Добавляет новую сущность в базу данных
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        /// <returns>Добавленная сущность с заполненным идентификатором (Id)</returns>
        T Add(T entity);

        /// <summary>
        /// Удаляет сущность по её идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности</param>
        /// <returns>True, если удаление прошло успешно; иначе False</returns>
        bool Delete(int id);

        /// <summary>
        /// Возвращает список всех сущностей из базы данных
        /// </summary>
        /// <returns>Список сущностей типа T</returns>
        List<T> ReadAll();

        /// <summary>
        /// Возвращает сущность по её идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности</param>
        /// <returns>Найденная сущность или null, если запись не найдена</returns>
        T? ReadById(int id);

        /// <summary>
        /// Обновляет данные существующей сущности
        /// </summary>
        /// <param name="entity">Сущность с обновлёнными данными</param>
        /// <returns>True, если обновление прошло успешно; иначе False</returns>
        bool Update(T entity);
    }
}