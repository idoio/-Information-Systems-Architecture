using AnimeFigures.Logic;
using AnimeFigures.Shared;
using Ninject;

namespace AnimeFigures.Presenter
{
    /// <summary>
    /// Точка сборки приложения (Composition Root).
    /// Единственное место, где создаётся IoC-контейнер и связываются все слои.
    /// По требованию задания размещена в сборке Presenter.
    /// </summary>
    public static class CompositionRoot
    {
        private static IKernel _kernel;

        /// <summary>
        /// Собирает приложение: создаёт контейнер, регистрирует View и возвращает готовый Presenter.
        /// </summary>
        /// <param name="view">Экземпляр главной формы (реализация IMainView)</param>
        /// <returns>Presenter, подписанный на события View и Model</returns>
        public static MainPresenter CreateMainPresenter(IMainView view)
        {
            _kernel = new StandardKernel(new SimpleConfigModule());

            // Регистрируем конкретный экземпляр формы как реализацию IMainView
            _kernel.Bind<IMainView>().ToConstant(view);

            // Ninject сам соберёт граф: Repository → Services → FigureLogic → Presenter
            return _kernel.Get<MainPresenter>();
        }

        /// <summary>
        /// Освобождает IoC-контейнер и все созданные им Singleton-объекты
        /// (например, DbContext в сценарии Entity Framework).
        /// </summary>
        public static void Shutdown()
        {
            _kernel?.Dispose();
            _kernel = null;
        }
    }
}