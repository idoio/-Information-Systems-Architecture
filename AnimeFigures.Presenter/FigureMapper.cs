using AnimeFigures.Model;
using AnimeFigures.Shared;

namespace AnimeFigures.Presenter
{
    /// <summary>
    /// Маппер: преобразует доменные объекты Figure в DTO для передачи во View.
    /// Благодаря этому View не знает о существовании доменного класса.
    /// </summary>
    internal static class FigureMapper
    {
        /// <summary>
        /// Преобразует доменную фигурку в DTO
        /// </summary>
        /// <param name="figure">Доменная модель фигурки</param>
        /// <returns>Объект FigureDto с данными для отображения</returns>
        public static FigureDto ToDto(Figure figure)
        {
            return new FigureDto
            {
                Id = figure.Id,
                Name = figure.Name,
                Character = figure.Character,
                Anime = figure.Anime,
                Manufacturer = figure.Manufacturer,
                Price = figure.Price,
                Scale = figure.Scale,
                Height = figure.Height,
                ReleaseDate = figure.ReleaseDate,
                InStock = figure.InStock
            };
        }

        /// <summary>
        /// Преобразует список доменных фигурок в список DTO
        /// </summary>
        /// <param name="figures">Коллекция доменных моделей фигурок</param>
        /// <returns>Список объектов FigureDto для отображения</returns>
        public static List<FigureDto> ToDtoList(IEnumerable<Figure> figures)
        {
            return figures.Select(ToDto).ToList();
        }
    }
}