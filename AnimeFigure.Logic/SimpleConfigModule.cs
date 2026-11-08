using AnimeFigures.DataAccessLayer;
using AnimeFigures.Model;
using Ninject.Modules;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Модуль конфигурации Ninject для регистрации зависимостей
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Переключатель сценариев БД. true — Dapper, false — Entity Framework
        /// </summary>
        private const bool UseDapper = true;

        /// <summary>
        /// Метод загрузки конфигурации зависимостей
        /// </summary>
        public override void Load()
        {
            // Регистрируем репозиторий как Singleton
            if (UseDapper)
            {
                // Сценарий Dapper:
                // Связываем интерфейс IRepository<Figure> с конкретной реализацией DapperFigureRepository
                Bind<IRepository<Figure>>().To<DapperFigureRepository>().InSingletonScope();
            }
            else
            {
                // Сценарий Entity Framework:
                Bind<AppDbContext>().ToSelf().InSingletonScope();
                Bind<IRepository<Figure>>().To<EfRepository<Figure>>().InSingletonScope();
            }

            // Регистрируем новые сервисы логики
            Bind<IFigureCrudService>().To<FigureCrudService>().InSingletonScope();
            Bind<IFigureAnalyticsService>().To<FigureAnalyticsService>().InSingletonScope();
        }
    }
}