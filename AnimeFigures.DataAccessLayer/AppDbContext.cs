using AnimeFigures.Model;
using Microsoft.EntityFrameworkCore;

namespace AnimeFigures.DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных для Entity Framework
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Figure> Figures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Используем локальный файл SQLite для простоты демонстрации
            optionsBuilder.UseSqlite("Data Source=figures.db");
        }
    }
}