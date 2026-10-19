using AnimeFigures.Model;
using Microsoft.EntityFrameworkCore;

namespace AnimeFigures.DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных для Entity Framework
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Таблица фигурок в базе данных
        /// </summary>
        public DbSet<Figure> Figures { get; set; } = null!;

        /// <summary>
        /// Настраивает подключение к базе данных SQLite
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=figures.db");
        }
    }
}