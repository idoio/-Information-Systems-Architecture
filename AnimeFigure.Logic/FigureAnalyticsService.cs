using AnimeFigures.DataAccessLayer;
using AnimeFigures.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Сервис для бизнес-функций (аналитика) с фигурками
    /// </summary>
    public class FigureAnalyticsService : IFigureAnalyticsService
    {
        private readonly IRepository<Figure> _repository;

        public FigureAnalyticsService(IRepository<Figure> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Группирует фигурки по производителю
        /// </summary>
        /// <returns>Словарь: производитель → список фигурок</returns>
        public Dictionary<string, List<Figure>> GroupByManufacturer()
        {
            return _repository.ReadAll()
                .GroupBy(f => f.Manufacturer)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Фильтрует фигурки по масштабу
        /// </summary>
        /// <param name="scale">Масштаб для поиска</param>
        /// <returns>Список фигурок</returns>
        public List<Figure> FilterByScale(string scale)
        {
            // Валидацию масштаба делает FigureCrudService или сам FigureLogic до вызова этого метода.
            // Здесь мы просто нормализуем строку и фильтруем.
            string normalized = scale.Trim().Replace(':', '/');

            return _repository.ReadAll()
                .Where(f => f.Scale.Replace(':', '/').Equals(normalized, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}