using AnimeFigures.Model;
using System;
using System.Collections.Generic;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Интерфейс для CRUD-операций с фигурками
    /// </summary>
    public interface IFigureCrudService
    {
        /// <summary>
        /// Создаёт новую фигурку
        /// </summary>
        Figure CreateFigure(string name, string character, string anime,
                           string manufacturer, decimal price, string scale,
                           double height, DateTime releaseDate, int inStock);

        /// <summary>
        /// Читает фигурку по ID
        /// </summary>
        Figure ReadFigure(int id);

        /// <summary>
        /// Получает список всех фигурок
        /// </summary>
        List<Figure> GetAllFigures();

        /// <summary>
        /// Обновляет данные фигурки
        /// </summary>
        bool UpdateFigure(int id, string name, string character, string anime,
                         string manufacturer, decimal price, string scale,
                         double height, DateTime releaseDate, int inStock);

        /// <summary>
        /// Удаляет фигурку по ID
        /// </summary>
        bool DeleteFigure(int id);

        /// <summary>
        /// Добавляет тестовые данные (вспомогательный метод)
        /// </summary>
        void AddDefaultFigures();
    }
}