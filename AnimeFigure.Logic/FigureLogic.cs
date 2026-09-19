using AnimeFigures.Model;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Класс бизнес-логики для управления коллекцией аниме-фигурок
    /// </summary>
    public class FigureLogic
    {
        // Приватное поле — UI не может напрямую изменить
        private readonly List<Figure> _figures;
        private int _nextId;

        /// <summary>
        /// Конструктор класса FigureLogic
        /// </summary>
        public FigureLogic()
        {
            _figures = new List<Figure>();
            _nextId = 1;
        }

        #region CRUD-операции

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
        public Figure CreateFigure(string name, string character, string anime,
                                   string manufacturer, decimal price, string scale,
                                   double height, DateTime releaseDate, int inStock)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название не может быть пустым");
            if (string.IsNullOrWhiteSpace(manufacturer))
                throw new ArgumentException("Производитель не может быть пустым");
            if (string.IsNullOrWhiteSpace(scale))
                throw new ArgumentException("Масштаб не может быть пустым");
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной");
            if (inStock < 0)
                throw new ArgumentException("Количество на складе не может быть отрицательным");

            var figure = new Figure(_nextId++, name, character, anime,
                                    manufacturer, price, scale, height,
                                    releaseDate, inStock);
            _figures.Add(figure);
            return figure;
        }

        /// <summary>
        /// Читает фигурку по её ID
        /// </summary>
        /// <param name="id">Уникальный идентификатор фигурки</param>
        /// <returns>Объект Figure или null, если не найдена</returns>
        public Figure ReadFigure(int id)
        {
            return _figures.FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Получает список всех фигурок
        /// </summary>
        /// <returns>Копия списка всех фигурок</returns>
        public List<Figure> GetAllFigures()
        {
            // Возвращаем копию, чтобы UI не мог изменить оригинал
            return new List<Figure>(_figures);
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
        /// <param name="inStock">Новое количество на складе</param>
        /// <returns>True, если обновление успешно</returns>
        public bool UpdateFigure(int id, string name, string character, string anime,
                                 string manufacturer, decimal price, string scale,
                                 double height, int inStock)
        {
            var figure = ReadFigure(id);
            if (figure == null)
                return false;

            // Валидация
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название не может быть пустым");
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной");
            if (inStock < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            figure.Name = name;
            figure.Character = character;
            figure.Anime = anime;
            figure.Manufacturer = manufacturer;
            figure.Price = price;
            figure.Scale = scale;
            figure.Height = height;
            figure.InStock = inStock;
            return true;
        }

        /// <summary>
        /// Удаляет фигурку по ID
        /// </summary>
        /// <param name="id">ID фигурки для удаления</param>
        /// <returns>True, если удаление успешно</returns>
        public bool DeleteFigure(int id)
        {
            var figure = ReadFigure(id);
            if (figure == null)
                return false;

            _figures.Remove(figure);
            return true;
        }

        #endregion

        #region Бизнес-функции

        /// <summary>
        /// Группирует фигурки по производителю с подсчётом количества
        /// </summary>
        /// <returns>Словарь: ключ — производитель, значение — количество фигурок</returns>
        public Dictionary<string, int> GroupByManufacturer()
        {
            return _figures
                .GroupBy(f => f.Manufacturer)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Фильтрует фигурки по заданному масштабу
        /// </summary>
        /// <param name="scale">Масштаб для поиска (например, "1/7", "Nendoroid")</param>
        /// <returns>Список фигурок указанного масштаба</returns>
        public List<Figure> FilterByScale(string scale)
        {
            if (string.IsNullOrWhiteSpace(scale))
                throw new ArgumentException("Масштаб не может быть пустым");

            return _figures
                .Where(f => f.Scale.Equals(scale, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        #endregion
    }
}