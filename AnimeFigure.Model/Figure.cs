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
        public string Name { get; set; }

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public string Character { get; set; }

        /// <summary>
        /// Название аниме
        /// </summary>
        public string Anime { get; set; }

        /// <summary>
        /// Производитель фигурки
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Цена фигурки в рублях
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Масштаб фигурки (1/4, 1/7, 1/8, Nendoroid и т.д.)
        /// </summary>
        public string Scale { get; set; }

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
        public override string ToString()
        {
            return $"[{Id}] {Name} ({Character} из {Anime}) — {Manufacturer}, {Scale}, {Price} ₽";
        }
    }
}