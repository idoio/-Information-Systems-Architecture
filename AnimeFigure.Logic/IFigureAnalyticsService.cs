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
        /// Группирует фигурки по производителю.
        /// Возвращает словарь: ключ — производитель, значение — список фигурок этого производителя.
        /// </summary>
        /// <returns>Словарь: производитель → список фигурок</returns>
        Dictionary<string, List<Figure>> GroupByManufacturer();

        /// <summary>
        /// Фильтрует фигурки по заданному масштабу (регистронезависимо).
        /// Масштаб нормализуется: ':' → '/'.
        /// </summary>
        /// <param name="scale">Масштаб для поиска (например, "1/7", "Nendoroid")</param>
        /// <returns>Список фигурок указанного масштаба</returns>
        List<Figure> FilterByScale(string scale);
    }
}