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
        private const bool UseDapper = true;

        public override void Load()
        {
            // Регистрируем репозиторий как Singleton
            if (UseDapper)
            {
<<<<<<< HEAD
=======
                // Сценарий Dapper:
                // Связываем интерфейс IRepository<Figure> с конкретной реализацией DapperFigureRepository
                // .InSingletonScope() гарантирует, что объект будет создан только один раз
>>>>>>> f3b611da7adab95b2d58216d3ac765017d517620
                Bind<IRepository<Figure>>().To<DapperFigureRepository>().InSingletonScope();
            }
            else
            {
                Bind<AppDbContext>().ToSelf().InSingletonScope();
                Bind<IRepository<Figure>>().To<EfRepository<Figure>>().InSingletonScope();
            }

            // Регистрируем сервисы
            Bind<IFigureCrudService>().To<FigureCrudService>().InSingletonScope();
            Bind<IFigureAnalyticsService>().To<FigureAnalyticsService>().InSingletonScope();
        }
    }
}