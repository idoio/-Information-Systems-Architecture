using AnimeFigures.DataAccessLayer;
using AnimeFigures.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Сервис для бизнес-функций (аналитика) с фигурками
    /// </summary>
    public class FigureAnalyticsService : IFigureAnalyticsService
    {
        private readonly IRepository<Figure> _repository;

        private static readonly string[] AllowedFractionScales = { "1/4", "1/6", "1/7", "1/8", "1/10" };
        private static readonly string[] AllowedNamedScales = { "nendoroid", "figuarts", "figma", "scale" };

        public FigureAnalyticsService(IRepository<Figure> repository)
        {
            _repository = repository;
        }

        public Dictionary<string, List<Figure>> GroupByManufacturer()
        {
            return _repository.ReadAll()
                .GroupBy(f => f.Manufacturer)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public List<Figure> FilterByScale(string scale)
        {
            ValidateScale(scale);
            string normalized = scale.Trim().Replace(':', '/');

            return _repository.ReadAll()
                .Where(f => f.Scale.Replace(':', '/').Equals(normalized, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private static void ValidateScale(string scale)
        {
            if (!FigureCrudService.TryValidateScale(scale, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}