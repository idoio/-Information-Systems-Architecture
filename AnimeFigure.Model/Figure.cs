namespace AnimeFigures.Model
{
    /// <summary>
    /// Класс, представляющий аниме-фигурку в магазине
    /// </summary>
    public class Figure
    {
        /// <summary>
        /// Уникальный идентификатор фигурки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название фигурки
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public string Character { get; set; } = string.Empty;

        /// <summary>
        /// Название аниме
        /// </summary>
        public string Anime { get; set; } = string.Empty;

        /// <summary>
        /// Производитель фигурки
        /// </summary>
        public string Manufacturer { get; set; } = string.Empty;

        /// <summary>
        /// Цена фигурки в рублях
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Масштаб фигурки (1/4, 1/7, 1/8, Nendoroid и т.д.)
        /// </summary>
        public string Scale { get; set; } = string.Empty;

        /// <summary>
        /// Высота фигурки в сантиметрах
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Дата выхода фигурки
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Количество на складе
        /// </summary>
        public int InStock { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Figure()
        {
            InStock = 0;
        }

        /// <summary>
        /// Конструктор с основными параметрами
        /// </summary>
        /// <param name="id">Уникальный идентификатор фигурки</param>
        /// <param name="name">Название фигурки</param>
        /// <param name="character">Имя персонажа</param>
        /// <param name="anime">Название аниме</param>
        /// <param name="manufacturer">Производитель</param>
        /// <param name="price">Цена</param>
        /// <param name="scale">Масштаб</param>
        /// <param name="height">Высота в см</param>
        /// <param name="releaseDate">Дата выхода</param>
        /// <param name="inStock">Количество на складе</param>
        public Figure(int id, string name, string character, string anime,
                      string manufacturer, decimal price, string scale,
                      double height, DateTime releaseDate, int inStock)
        {
            Id = id;
            Name = name;
            Character = character;
            Anime = anime;
            Manufacturer = manufacturer;
            Price = price;
            Scale = scale;
            Height = height;
            ReleaseDate = releaseDate;
            InStock = inStock;
        }

        /// <summary>
        /// Переопределение ToString для удобного отображения
        /// </summary>
        /// <returns>Строка с информацией о фигурке</returns>
        public override string ToString()
        {
            return $"[{Id}] {Name} ({Character} из {Anime}) — {Manufacturer}, {Scale}, {Price}р., " +
                   $"в наличии: {InStock} шт., дата выхода: {ReleaseDate:dd.MM.yyyy}";
        }
    }
}