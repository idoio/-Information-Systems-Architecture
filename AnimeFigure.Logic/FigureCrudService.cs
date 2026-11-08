using AnimeFigures.DataAccessLayer;
using AnimeFigures.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Сервис для CRUD-операций с фигурками
    /// </summary>
    public class FigureCrudService : IFigureCrudService
    {
        private readonly IRepository<Figure> _repository;

        /// <summary>
        /// Допустимые дробные масштабы
        /// </summary>
        private static readonly string[] AllowedFractionScales = { "1/4", "1/6", "1/7", "1/8", "1/10" };

        /// <summary>
        /// Допустимые именованные масштабы
        /// </summary>
        private static readonly string[] AllowedNamedScales = { "nendoroid", "figuarts", "figma", "scale" };

        public FigureCrudService(IRepository<Figure> repository)
        {
            _repository = repository;
        }

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

            var allFigures = _repository.ReadAll();
            int newId = allFigures.Count > 0 ? allFigures.Max(f => f.Id) + 1 : 1;

            var figure = new Figure(newId, name, character, anime,
                                    manufacturer, price, scale, height,
                                    releaseDate, inStock);
            return _repository.Add(figure);
        }

        public Figure ReadFigure(int id)
        {
            return _repository.ReadById(id);
        }

        public List<Figure> GetAllFigures()
        {
            return _repository.ReadAll();
        }

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

        public bool DeleteFigure(int id)
        {
            var figure = ReadFigure(id);
            if (figure == null)
                return false;

            return _repository.Delete(id);
        }

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