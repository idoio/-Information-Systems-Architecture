namespace AnimeFigures.Shared
{
    /// <summary>
    /// DTO (Data Transfer Object) для передачи данных о фигурке между Presenter и View.
    /// Позволяет View не зависеть от доменного класса Figure.
    /// </summary>
    public class FigureDto
    {
        /// <summary>Уникальный идентификатор фигурки</summary>
        public int Id { get; set; }

        /// <summary>Название фигурки</summary>
        public string Name { get; set; } = "";

        /// <summary>Имя персонажа</summary>
        public string Character { get; set; } = "";

        /// <summary>Название аниме</summary>
        public string Anime { get; set; } = "";

        /// <summary>Производитель</summary>
        public string Manufacturer { get; set; } = "";

        /// <summary>Цена фигурки</summary>
        public decimal Price { get; set; }

        /// <summary>Масштаб</summary>
        public string Scale { get; set; } = "";

        /// <summary>Высота в см</summary>
        public double Height { get; set; }

        /// <summary>Дата выхода</summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>Количество на складе</summary>
        public int InStock { get; set; }
    }
}