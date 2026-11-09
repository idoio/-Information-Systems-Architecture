using AnimeFigures.Model;
using System;
using System.Collections.Generic;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Интерфейс для бизнес-логики (фасад), объединяющий CRUD и аналитику
    /// </summary>
    public interface IFigureLogic
    {
        /// <summary>
        /// Создаёт новую фигурку
        /// </summary>
        /// <param name="name">Название фигурки</param>
        /// <param name="character">Имя персонажа</param>
        /// <param name="anime">Название аниме</param>
        /// <param name="manufacturer">Производитель</param>
        /// <param name="price">Цена</param>
        /// <param name="scale">Масштаб</param>
        /// <param name="height">Высота в см</param>
        /// <param name="releaseDate">Дата выхода</param>
        /// <param name="inStock">Количество на складе</param>
        /// <returns>Созданный объект Figure</returns>
        Figure CreateFigure(string name, string character, string anime,
                            string manufacturer, decimal price, string scale,
                            double height, DateTime releaseDate, int inStock);

        /// <summary>
        /// Читает фигурку по её ID
        /// </summary>
        /// <param name="id">Уникальный идентификатор фигурки</param>
        /// <returns>Объект Figure или null, если не найдена</returns>
        Figure ReadFigure(int id);

        /// <summary>
        /// Получает список всех фигурок
        /// </summary>
        /// <returns>Список всех фигурок</returns>
        List<Figure> GetAllFigures();

        /// <summary>
        /// Обновляет данные существующей фигурки
        /// </summary>
        /// <returns>True, если обновление успешно</returns>
        bool UpdateFigure(int id, string name, string character, string anime,
                          string manufacturer, decimal price, string scale,
                          double height, DateTime releaseDate, int inStock);

        /// <summary>
        /// Удаляет фигурку по ID
        /// </summary>
        /// <param name="id">ID фигурки для удаления</param>
        /// <returns>True, если удаление успешно</returns>
        bool DeleteFigure(int id);

        /// <summary>
        /// Добавляет тестовые данные
        /// </summary>
        void AddDefaultFigures();

        /// <summary>
        /// Группирует фигурки по производителю
        /// </summary>
        /// <returns>Словарь: производитель → список фигурок</returns>
        Dictionary<string, List<Figure>> GroupByManufacturer();

        /// <summary>
        /// Фильтрует фигурки по заданному масштабу
        /// </summary>
        /// <param name="scale">Масштаб для поиска</param>
        /// <returns>Список фигурок указанного масштаба</returns>
        List<Figure> FilterByScale(string scale);
    }
}