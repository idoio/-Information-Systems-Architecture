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
        /// Допустимые дробные масштабы (в нормализованном виде через '/').
        /// </summary>
        private static readonly string[] AllowedFractionScales = { "1/4", "1/6", "1/7", "1/8", "1/10" };

        /// <summary>
        /// Допустимые именованные масштабы (в нижнем регистре).
        /// </summary>
        private static readonly string[] AllowedNamedScales = { "nendoroid", "figuarts", "figma", "scale" };

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
        public Figure CreateFigure(string name, string character, string anime,
                                   string manufacturer, decimal price, string scale,
                                   double height, DateTime releaseDate, int inStock)
        {
            ValidateString(name, "Название");
            ValidateString(character, "Персонаж");
            ValidateString(anime, "Аниме");
            ValidateString(manufacturer, "Производитель");
            ValidateScale(scale);
            ValidatePrice(price);
            ValidateInStock(inStock);
            ValidateHeight(height);

            var figure = new Figure(_nextId++, name, character, anime,
                                    manufacturer, price, scale, height,
                                    releaseDate, inStock);
            _figures.Add(figure);
            return figure;
        }

        /// <summary>
        /// Читает фигурку по её ID
        /// </summary>
        public Figure ReadFigure(int id)
        {
            return _figures.FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Получает список всех фигурок.
        /// Возвращает новый список, но объекты Figure внутри — те же самые.
        /// Для полной изоляции нужно клонировать каждую фигурку.
        /// </summary>
        public List<Figure> GetAllFigures()
        {
            return new List<Figure>(_figures);
        }

        /// <summary>
        /// Обновляет данные существующей фигурки
        /// </summary>
        public bool UpdateFigure(int id, string name, string character, string anime,
                                 string manufacturer, decimal price, string scale,
                                 double height, DateTime releaseDate, int inStock)
        {
            var figure = ReadFigure(id);
            if (figure == null)
                return false;

            ValidateString(name, "Название");
            ValidateString(character, "Персонаж");
            ValidateString(anime, "Аниме");
            ValidateString(manufacturer, "Производитель");
            ValidateScale(scale);
            ValidatePrice(price);
            ValidateInStock(inStock);
            ValidateHeight(height);

            figure.Name = name;
            figure.Character = character;
            figure.Anime = anime;
            figure.Manufacturer = manufacturer;
            figure.Price = price;
            figure.Scale = scale;
            figure.Height = height;
            figure.ReleaseDate = releaseDate;
            figure.InStock = inStock;
            return true;
        }

        /// <summary>
        /// Удаляет фигурку по ID
        /// </summary>
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
        public Dictionary<string, int> GroupByManufacturer()
        {
            return _figures
                .GroupBy(f => f.Manufacturer)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Фильтрует фигурки по заданному масштабу (регистронезависимо).
        /// Масштаб нормализуется: ':' → '/'.
        /// </summary>
        public List<Figure> FilterByScale(string scale)
        {
            ValidateScale(scale);
            string normalized = scale.Trim().Replace(':', '/');

            return _figures
                .Where(f => f.Scale.Replace(':', '/').Equals(normalized, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Добавляет тестовые данные для демонстрации работы приложения.
        /// Это вспомогательный метод, не часть бизнес-логики.
        /// </summary>
        public void AddDefaultFigures()
        {
            if (_figures.Count > 0) return;

            CreateFigure("Наруто Узумаки", "Наруто", "Наруто", "Good Smile Company", 15000m, "1/7", 25.0, new DateTime(2023, 5, 15), 5);
            CreateFigure("Сон Гоку", "Гоку", "Жемчуг дракона", "Bandai", 8500m, "Nendoroid", 10.0, new DateTime(2022, 11, 20), 12);
            CreateFigure("Луффи", "Луффи", "Ван Пис", "MegaHouse", 22000m, "1/8", 22.0, new DateTime(2023, 8, 10), 3);
            CreateFigure("Саске Учиха", "Саске", "Наруто", "Good Smile Company", 14500m, "1/7", 24.0, new DateTime(2023, 6, 1), 7);
            CreateFigure("Танджиро Камадо", "Танджиро", "Истребитель демонов", "Aniplex", 35000m, "1/4", 45.0, new DateTime(2024, 1, 15), 2);
            CreateFigure("Гоку Супер Сайян", "Гоку", "Жемчуг дракона", "Bandai", 12000m, "1/7", 20.0, new DateTime(2023, 3, 10), 8);
            CreateFigure("Незуко Камадо", "Незуко", "Истребитель демонов", "Good Smile Company", 9000m, "Nendoroid", 11.0, new DateTime(2023, 9, 5), 15);
            CreateFigure("Роронао Зоро", "Зоро", "Ван Пис", "MegaHouse", 18000m, "1/7", 23.0, new DateTime(2023, 7, 20), 4);
        }

        #endregion

        #region Валидация

        private static void ValidateString(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} не может быть пустым");
        }

        private static void ValidatePrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной");
        }

        private static void ValidateInStock(int inStock)
        {
            if (inStock < 0)
                throw new ArgumentException("Количество на складе не может быть отрицательным");
        }

        private static void ValidateHeight(double height)
        {
            if (height < 0)
                throw new ArgumentException("Высота не может быть отрицательной");
        }

        /// <summary>
        /// Проверяет, что масштаб задан в допустимом формате и входит в список разрешённых.
        /// Допустимо: "1/4", "1/6", "1/7", "1/8", "1/10", "Nendoroid", "Figuarts", "Figma", "Scale".
        /// Разделитель ':' нормализуется в '/'.
        /// </summary>
        private static void ValidateScale(string scale)
        {
            if (string.IsNullOrWhiteSpace(scale))
                throw new ArgumentException("Масштаб не может быть пустым");

            string normalized = scale.Trim().Replace(':', '/');

            if (normalized.Contains('/'))
            {
                if (AllowedFractionScales.Contains(normalized))
                    return;

                throw new ArgumentException(
                    $"Недопустимый масштаб '{scale}'. Допустимые дробные: {string.Join(", ", AllowedFractionScales)}");
            }

            if (AllowedNamedScales.Contains(normalized.ToLower()))
                return;

            throw new ArgumentException(
                $"Недопустимый масштаб '{scale}'. Допустимые: {string.Join(", ", AllowedFractionScales)}, " +
                $"{string.Join(", ", AllowedNamedScales)}");
        }

        #endregion
    }
}