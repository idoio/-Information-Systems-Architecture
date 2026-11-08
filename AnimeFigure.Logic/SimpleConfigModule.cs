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