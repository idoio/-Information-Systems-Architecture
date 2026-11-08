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

        /// <summary>
        /// Конструктор сервиса аналитики
        /// </summary>
        /// <param name="repository">Репозиторий для получения данных из БД</param>
        public FigureAnalyticsService(IRepository<Figure> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Группирует фигурки по производителю.
        /// Возвращает словарь: ключ — производитель, значение — список фигурок этого производителя.
        /// </summary>
        /// <returns>Словарь: производитель - список фигурок</returns>
        public Dictionary<string, List<Figure>> GroupByManufacturer()
        {
            return _repository.ReadAll()
                .GroupBy(f => f.Manufacturer)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Фильтрует фигурки по заданному масштабу (регистронезависимо).
        /// Масштаб нормализуется: ':' - '/'.
        /// </summary>
        /// <param name="scale">Масштаб для поиска (например, "1/7", "Nendoroid")</param>
        /// <returns>Список фигурок указанного масштаба</returns>
        public List<Figure> FilterByScale(string scale)
        {
            // Валидация масштаба делегирована FigureCrudService или FigureLogic
            // Здесь мы только нормализуем строку и фильтруем данные
            string normalized = scale.Trim().Replace(':', '/');

            return _repository.ReadAll()
                .Where(f => f.Scale.Replace(':', '/').Equals(normalized, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}