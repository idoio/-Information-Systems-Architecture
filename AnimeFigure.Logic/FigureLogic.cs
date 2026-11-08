using AnimeFigures.Model;
using System;
using System.Collections.Generic;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Класс бизнес-логики (фасад), который связывает сервисы CRUD и Аналитики
    /// </summary>
    public class FigureLogic
    {
        private readonly IFigureCrudService _crudService;
        private readonly IFigureAnalyticsService _analyticsService;

        /// <summary>
        /// Конструктор класса FigureLogic (внедрение зависимостей)
        /// </summary>
        /// <param name="crudService">Сервис CRUD-операций</param>
        /// <param name="analyticsService">Сервис аналитики</param>
        public FigureLogic(IFigureCrudService crudService, IFigureAnalyticsService analyticsService)
        {
            _crudService = crudService;
            _analyticsService = analyticsService;
        }

        #region CRUD-операции (делегируются в CrudService)

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
        public Figure CreateFigure(string name, string character, string anime,
                                   string manufacturer, decimal price, string scale,
                                   double height, DateTime releaseDate, int inStock)
        {
            return _crudService.CreateFigure(name, character, anime, manufacturer, price, scale, height, releaseDate, inStock);
        }

        /// <summary>
        /// Читает фигурку по её ID
        /// </summary>
        /// <param name="id">Уникальный идентификатор фигурки</param>
        /// <returns>Объект Figure или null, если не найдена</returns>
        public Figure ReadFigure(int id)
        {
            return _crudService.ReadFigure(id);
        }

        /// <summary>
        /// Получает список всех фигурок
        /// </summary>
        /// <returns>Список всех фигурок</returns>
        public List<Figure> GetAllFigures()
        {
            return _crudService.GetAllFigures();
        }

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
        public bool UpdateFigure(int id, string name, string character, string anime,
                                 string manufacturer, decimal price, string scale,
                                 double height, DateTime releaseDate, int inStock)
        {
            return _crudService.UpdateFigure(id, name, character, anime, manufacturer, price, scale, height, releaseDate, inStock);
        }

        /// <summary>
        /// Удаляет фигурку по ID
        /// </summary>
        /// <param name="id">ID фигурки для удаления</param>
        /// <returns>True, если удаление успешно</returns>
        public bool DeleteFigure(int id)
        {
            return _crudService.DeleteFigure(id);
        }

        /// <summary>
        /// Добавляет тестовые данные
        /// </summary>
        public void AddDefaultFigures()
        {
            _crudService.AddDefaultFigures();
        }

        #endregion

        #region Бизнес-функции (делегируются в AnalyticsService)

        /// <summary>
        /// Группирует фигурки по производителю
        /// </summary>
        /// <returns>Словарь: производитель → список фигурок</returns>
        public Dictionary<string, List<Figure>> GroupByManufacturer()
        {
            return _analyticsService.GroupByManufacturer();
        }

        /// <summary>
        /// Фильтрует фигурки по заданному масштабу
        /// </summary>
        /// <param name="scale">Масштаб для поиска</param>
        /// <returns>Список фигурок указанного масштаба</returns>
        public List<Figure> FilterByScale(string scale)
        {
            return _analyticsService.FilterByScale(scale);
        }

        #endregion

        #region Валидация (прокси-метод для UI)

        /// <summary>
        /// Проверяет корректность масштаба (используется в UI)
        /// </summary>
        /// <param name="scale">Проверяемый масштаб</param>
        /// <param name="errorMessage">Текст ошибки</param>
        /// <returns>True, если масштаб допустим</returns>
        public static bool TryValidateScale(string scale, out string errorMessage)
        {
            // Перенаправляем вызов в CrudService, чтобы не сломать Program.cs
            return FigureCrudService.TryValidateScale(scale, out errorMessage);
        }

        #endregion
    }
}