using AnimeFigures.Model;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace AnimeFigures.DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория для сущности Figure с использованием микро-ORM Dapper.
    /// Является конкретной реализацией, так как Dapper требует явного написания SQL-запросов.
    /// </summary>
    public class DapperFigureRepository : IRepository<Figure>
    {
        private readonly string _connectionString;
        private const string TableName = "Figures"; // Явно задаем имя таблицы

        /// <summary>
        /// Конструктор репозитория Dapper
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        public DapperFigureRepository(string connectionString = "Data Source=figures.db")
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }

        /// <summary>
        /// Инициализирует таблицу в базе данных (создает, если нет)
        /// </summary>
        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var sql = $@"
                    CREATE TABLE IF NOT EXISTS {TableName} (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT, Character TEXT, Anime TEXT, Manufacturer TEXT,
                        Price REAL, Scale TEXT, Height REAL, ReleaseDate TEXT, InStock INTEGER
                    )";
                connection.Execute(sql);
            }
        }

        /// <summary>
        /// Добавляет фигурку и возвращает её с заполненным Id
        /// </summary>
        public Figure Add(Figure entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var sql = $@"INSERT INTO {TableName} (Name, Character, Anime, Manufacturer, Price, Scale, Height, ReleaseDate, InStock) 
                             VALUES (@Name, @Character, @Anime, @Manufacturer, @Price, @Scale, @Height, @ReleaseDate, @InStock);
                             SELECT last_insert_rowid();";

                var id = connection.QuerySingle<int>(sql, entity);
                entity.Id = id;
                return entity;
            }
        }

        /// <summary>
        /// Удаляет фигурку по Id. Возвращает true, если удаление успешно
        /// </summary>
        public bool Delete(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var sql = $"DELETE FROM {TableName} WHERE Id = @Id";

                int affectedRows = connection.Execute(sql, new { Id = id });
                return affectedRows > 0;
            }
        }

        /// <summary>
        /// Возвращает список всех фигурок
        /// </summary>
        public List<Figure> ReadAll()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var sql = $"SELECT * FROM {TableName}";

                return connection.Query<Figure>(sql).ToList();
            }
        }

        /// <summary>
        /// Возвращает фигурку по Id или null, если не найдена
        /// </summary>
        public Figure? ReadById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";

                // QuerySingleOrDefault вернет null, если запись не найдена
                return connection.QuerySingleOrDefault<Figure>(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Обновляет данные фигурки. Возвращает true, если обновление успешно
        /// </summary>
        public bool Update(Figure entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var sql = $@"UPDATE {TableName} SET 
                             Name = @Name, Character = @Character, Anime = @Anime, Manufacturer = @Manufacturer, 
                             Price = @Price, Scale = @Scale, Height = @Height, ReleaseDate = @ReleaseDate, InStock = @InStock 
                             WHERE Id = @Id";

                int affectedRows = connection.Execute(sql, entity);
                return affectedRows > 0;
            }
        }
    }
}