namespace AnimeFigures.Shared
{
    /// <summary>
    /// DTO (Data Transfer Object) для передачи данных о фигурке между Presenter и View.
    /// Позволяет View не зависеть от доменного класса Figure.
    /// </summary>
    public class FigureDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Character { get; set; } = "";
        public string Anime { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public decimal Price { get; set; }
        public string Scale { get; set; } = "";
        public double Height { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int InStock { get; set; }
    }
}