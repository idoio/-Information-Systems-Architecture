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
        /// Создаёт новую фигурку и добавляет её в коллекцию
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
        /// <exception cref="ArgumentException">
        /// Бросается при пустых строковых полях, недопустимом масштабе
        /// или отрицательных числовых значениях.
        /// </exception>
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
        /// <param name="id">ID фигурки для обновления</param>
        /// <param name="name">Новое название</param>
        /// <param name="character">Новый персонаж</param>
        /// <param name="anime">Новое аниме</param>
        /// <param name="manufacturer">Новый производитель</param>
        /// <param name="price">Новая цена</param>
        /// <param name="scale">Новый масштаб</param>
        /// <param name="height">Новая высота</param>
        /// <param name="releaseDate">Новая дата выхода</param>
        /// <param name="inStock">Новое количество на складе</param>
        /// <returns>True, если обновление успешно</returns>
        /// <exception cref="ArgumentException">
        /// Бросается при пустых строковых полях, недопустимом масштабе
        /// или отрицательных числовых значениях.
        /// </exception>
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
        /// Добавляет тестовые данные (вспомогательный метод)
        /// </summary>
        void AddDefaultFigures();

    }
}