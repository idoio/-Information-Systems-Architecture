using AnimeFigures.Model;
using System.Collections.Generic;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Интерфейс для бизнес-функций (аналитика) с фигурками
    /// </summary>
    public interface IFigureAnalyticsService
    {
        /// <summary>
        /// Группирует фигурки по производителю
        /// </summary>
        Dictionary<string, List<Figure>> GroupByManufacturer();

        /// <summary>
        /// Фильтрует фигурки по масштабу
        /// </summary>
        List<Figure> FilterByScale(string scale);
    }
}