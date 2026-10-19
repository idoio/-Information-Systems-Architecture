using AnimeFigures.Model;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace AnimeFigures.DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория с использованием микро-ORM Dapper
    /// </summary>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        /// <summary>
        /// Конструктор репозитория Dapper
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        public DapperRepository(string connectionString = "Data Source=figures.db")
        {
            _connectionString = connectionString;
            // Имя таблицы берем из имени класса (например, "Figure" → "Figures")
            _tableName = typeof(T).Name + "s";

            // Инициализируем таблицу при первом запуске (для SQLite)
            InitializeDatabase();
        }

        /// <summary>
        /// Создаёт таблицу, если её ещё нет
        /// </summary>
        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $@"
                CREATE TABLE IF NOT EXISTS {_tableName} (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT, Character TEXT, Anime TEXT, Manufacturer TEXT,
                    Price REAL, Scale TEXT, Height REAL, ReleaseDate TEXT, InStock INTEGER
                )";
            connection.Execute(sql);
        }

        /// <summary>
        /// Добавляет сущность и возвращает её с заполненным Id
        /// </summary>
        public T Add(T entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $@"INSERT INTO {_tableName} (Name, Character, Anime, Manufacturer, Price, Scale, Height, ReleaseDate, InStock) 
                         VALUES (@Name, @Character, @Anime, @Manufacturer, @Price, @Scale, @Height, @ReleaseDate, @InStock);
                         SELECT last_insert_rowid();";

            var id = connection.QuerySingle<int>(sql, entity);
            entity.Id = id;
            return entity;
        }

        /// <summary>
        /// Удаляет сущность по Id. Возвращает true, если удаление успешно
        /// </summary>
        public bool Delete(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
            int affected = connection.Execute(sql, new { Id = id });
            return affected > 0;
        }

        /// <summary>
        /// Возвращает все сущности
        /// </summary>
        public List<T> ReadAll()
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"SELECT * FROM {_tableName}";
            return connection.Query<T>(sql).ToList();
        }

        /// <summary>
        /// Возвращает сущность по Id или null, если не найдена
        /// </summary>
        public T? ReadById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            return connection.QuerySingleOrDefault<T>(sql, new { Id = id });
        }

        /// <summary>
        /// Обновляет сущность. Возвращает true, если обновление успешно
        /// </summary>
        public bool Update(T entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = $@"UPDATE {_tableName} SET 
                         Name = @Name, Character = @Character, Anime = @Anime, Manufacturer = @Manufacturer, 
                         Price = @Price, Scale = @Scale, Height = @Height, ReleaseDate = @ReleaseDate, InStock = @InStock 
                         WHERE Id = @Id";
            int affected = connection.Execute(sql, entity);
            return affected > 0;
        }
    }
}