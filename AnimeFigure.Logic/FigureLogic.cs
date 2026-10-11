using AnimeFigures.Model;
using AnimeFigures.DataAccessLayer;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Класс бизнес-логики для управления коллекцией аниме-фигурок
    /// </summary>
    public class FigureLogic
    {
        // Приватное поле — UI не может напрямую изменить
        private readonly IRepository<Figure> _repository;
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
        public FigureLogic(IRepository<Figure> repository)
        {
            _repository = repository;
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
        /// <exception cref="ArgumentException">
        /// Бросается при пустых строковых полях, недопустимом масштабе
        /// или отрицательных числовых значениях.
        /// </exception>
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

            var allFigures = _repository.ReadAll().ToList();
            int newId = allFigures.Count > 0 ? allFigures.Max(f => f.Id) + 1 : 1;

            var figure = new Figure(newId, name, character, anime,
                                    manufacturer, price, scale, height,
                                    releaseDate, inStock);
            _repository.Add(figure);
            return figure;
        }

        /// <summary>
        /// Читает фигурку по её ID
        /// </summary>
        /// <param name="id">Уникальный идентификатор фигурки</param>
        /// <returns>Объект Figure или null, если не найдена</returns>
        public Figure ReadFigure(int id)
        {
            return _repository.ReadById(id);
        }

        /// <summary>
        /// Получает список всех фигурок
        /// </summary>
        /// <returns>Копия списка всех фигурок</returns>
        public List<Figure> GetAllFigures()
        {
            return _repository.ReadAll().ToList();
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
        /// <exception cref="ArgumentException">
        /// Бросается при пустых строковых полях, недопустимом масштабе
        /// или отрицательных числовых значениях.
        /// </exception>
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
            return _repository.Update(figure);
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

            _repository.Delete(id);
            return true;
        }

        #endregion

        #region Бизнес-функции

        /// <summary>
        /// Группирует фигурки по производителю.
        /// Возвращает словарь: ключ — производитель, значение — список фигурок этого производителя.
        /// </summary>
        /// <returns>Словарь: производитель → список фигурок</returns>
        public Dictionary<string, List<Figure>> GroupByManufacturer()
        {
            return _repository.ReadAll()
                .GroupBy(f => f.Manufacturer)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Фильтрует фигурки по заданному масштабу (регистронезависимо).
        /// Масштаб нормализуется: ':' → '/'.
        /// </summary>
        /// <param name="scale">Масштаб для поиска (например, "1/7", "Nendoroid")</param>
        /// <returns>Список фигурок указанного масштаба</returns>
        /// <exception cref="ArgumentException">
        /// Бросается, если масштаб пустой или не входит в список допустимых.
        /// </exception>
        public List<Figure> FilterByScale(string scale)
        {
            ValidateScale(scale);
            string normalized = scale.Trim().Replace(':', '/');

            return _repository.ReadAll()
                .Where(f => f.Scale.Replace(':', '/').Equals(normalized, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Добавляет тестовые данные для демонстрации работы приложения.
        /// Это вспомогательный метод, не часть бизнес-логики.
        /// </summary>
        public void AddDefaultFigures()
        {
            if (_repository.ReadAll().Any()) return;

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

        /// <summary>
        /// Проверяет, что строка не пустая
        /// </summary>
        /// <param name="value">Проверяемая строка</param>
        /// <param name="fieldName">Название поля для сообщения об ошибке</param>
        /// <exception cref="ArgumentException">Бросается, если строка пустая</exception>
        private static void ValidateString(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} не может быть пустым");
        }

        /// <summary>
        /// Проверяет, что цена неотрицательна
        /// </summary>
        /// <param name="price">Цена</param>
        /// <exception cref="ArgumentException">Бросается, если цена отрицательная</exception>
        private static void ValidatePrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной");
        }

        /// <summary>
        /// Проверяет, что количество на складе неотрицательно
        /// </summary>
        /// <param name="inStock">Количество на складе</param>
        /// <exception cref="ArgumentException">Бросается, если количество отрицательное</exception>
        private static void ValidateInStock(int inStock)
        {
            if (inStock < 0)
                throw new ArgumentException("Количество на складе не может быть отрицательным");
        }

        /// <summary>
        /// Проверяет, что высота неотрицательна
        /// </summary>
        /// <param name="height">Высота в см</param>
        /// <exception cref="ArgumentException">Бросается, если высота отрицательная</exception>
        private static void ValidateHeight(double height)
        {
            if (height < 0)
                throw new ArgumentException("Высота не может быть отрицательной");
        }

        /// <summary>
        /// Проверяет корректность масштаба без выброса исключения (паттерн TryParse).
        /// Допустимо: "1/4", "1/6", "1/7", "1/8", "1/10", "Nendoroid", "Figuarts", "Figma", "Scale".
        /// Разделитель ':' нормализуется в '/'.
        /// Используется в UI для удобного вывода ошибки пользователю.
        /// </summary>
        /// <param name="scale">Проверяемый масштаб</param>
        /// <param name="errorMessage">Текст ошибки, если масштаб недопустим</param>
        /// <returns>True, если масштаб допустим; иначе False</returns>
        public static bool TryValidateScale(string scale, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(scale))
            {
                errorMessage = "Масштаб не может быть пустым";
                return false;
            }

            string normalized = scale.Trim().Replace(':', '/');

            if (normalized.Contains('/'))
            {
                if (AllowedFractionScales.Contains(normalized))
                    return true;

                errorMessage = $"Недопустимый масштаб '{scale}'. Допустимые дробные: {string.Join(", ", AllowedFractionScales)}";
                return false;
            }

            if (AllowedNamedScales.Contains(normalized.ToLower()))
                return true;

            errorMessage = $"Недопустимый масштаб '{scale}'. Допустимые: {string.Join(", ", AllowedFractionScales)}, {string.Join(", ", AllowedNamedScales)}";
            return false;
        }

        /// <summary>
        /// Проверяет, что масштаб задан в допустимом формате и входит в список разрешённых.
        /// Допустимо: "1/4", "1/6", "1/7", "1/8", "1/10", "Nendoroid", "Figuarts", "Figma", "Scale".
        /// Разделитель ':' нормализуется в '/'.
        /// </summary>
        /// <param name="scale">Проверяемый масштаб</param>
        /// <exception cref="ArgumentException">
        /// Бросается, если масштаб пустой или не входит в список допустимых.
        /// </exception>
        private static void ValidateScale(string scale)
        {
            if (!TryValidateScale(scale, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }
        }

        #endregion
    }
}