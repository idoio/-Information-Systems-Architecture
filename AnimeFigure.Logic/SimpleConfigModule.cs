using AnimeFigures.DataAccessLayer;
using AnimeFigures.Model;
using Ninject.Modules;

namespace AnimeFigures.Logic
{
    /// <summary>
    /// Модуль конфигурации Ninject для регистрации зависимостей (DI)
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Переключатель сценариев БД. 
        /// true — используем Dapper, false — используем Entity Framework
        /// </summary>
        private const bool UseDapper = true;

        /// <summary>
        /// Метод загрузки конфигурации зависимостей
        /// </summary>
        public override void Load()
        {
            if (UseDapper)
            {
                // Сценарий Dapper:
                // Связываем интерфейс IRepository<Figure> с конкретной реализацией DapperFigureRepository
                // .InSingletonScope() гарантирует, что объект будет создан только один раз
                Bind<IRepository<Figure>>().To<DapperFigureRepository>().InSingletonScope();
            }
            else
            {
                // Сценарий Entity Framework:
                // EF требует DbContext, поэтому сначала регистрируем его
                Bind<AppDbContext>().ToSelf().InSingletonScope();

                // Затем регистрируем сам репозиторий
                Bind<IRepository<Figure>>().To<EfRepository<Figure>>().InSingletonScope();
            }
        }
    }
}