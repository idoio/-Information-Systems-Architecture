using AnimeFigures.Logic;
using AnimeFigures.Shared;

namespace AnimeFigures.Presenter
{
    /// <summary>
    /// Presenter главной формы. Подписан на события View и Model,
    /// вызывает бизнес-логику через интерфейс IFigureLogic (фасад)
    /// и перерисовывает View через интерфейс IMainView.
    /// </summary>
    public class MainPresenter: IDisposable
    {
        private readonly IMainView _view;
        private readonly IFigureLogic _logic;

        /// <summary>
        /// Конструктор Presenter. Зависимости внедряются через Ninject (принцип D).
        /// </summary>
        /// <param name="view">Представление (форма), с которым работает Presenter</param>
        /// <param name="logic">Фасад бизнес-логики (CRUD + аналитика)</param>
        public MainPresenter(IMainView view, IFigureLogic logic)
        {
            _view = view;
            _logic = logic;

            // Подписка на события View (View → Presenter)
            _view.CreateRequested += OnCreateRequested;
            _view.UpdateRequested += OnUpdateRequested;
            _view.DeleteRequested += OnDeleteRequested;
            _view.ShowAllRequested += OnShowAllRequested;
            _view.GroupByManufacturerRequested += OnGroupByManufacturerRequested;
            _view.FilterByScaleRequested += OnFilterByScaleRequested;
            _view.SelectionChanged += OnSelectionChanged;

            // Подписка на событие Model (Model → Presenter)
            _logic.FiguresChanged += OnModelFiguresChanged;

            // Начальная загрузка: тестовые данные + первичная отрисовка
            _logic.AddDefaultFigures();
            RefreshFigures();
        }

        // ===== Обработчики событий View =====

        /// <summary>
        /// Обрабатывает запрос на создание фигурки
        /// </summary>
        private void OnCreateRequested(FigureDto dto)
        {
            try
            {
                var figure = _logic.CreateFigure(dto.Name, dto.Character, dto.Anime,
                    dto.Manufacturer, dto.Price, dto.Scale, dto.Height, dto.ReleaseDate, dto.InStock);

                _view.ClearFields();
                _view.ShowMessage($"Фигурка добавлена с ID: {figure.Id}", "Успех");
            }
            catch (Exception ex)
            {
                _view.ShowMessage(ex.Message, "Ошибка", isError: true);
            }
        }

        /// <summary>
        /// Обрабатывает запрос на обновление фигурки
        /// </summary>
        private void OnUpdateRequested(int id, FigureDto dto)
        {
            try
            {
                bool success = _logic.UpdateFigure(id, dto.Name, dto.Character, dto.Anime,
                    dto.Manufacturer, dto.Price, dto.Scale, dto.Height, dto.ReleaseDate, dto.InStock);

                _view.ShowMessage(
                    success ? "Данные обновлены!" : "Фигурка не найдена.",
                    success ? "Успех" : "Внимание",
                    isError: !success);
            }
            catch (Exception ex)
            {
                _view.ShowMessage(ex.Message, "Ошибка", isError: true);
            }
        }

        /// <summary>
        /// Обрабатывает запрос на удаление фигурки
        /// </summary>
        private void OnDeleteRequested(int id)
        {
            try
            {
                bool success = _logic.DeleteFigure(id);
                _view.ShowMessage(
                    success ? "Фигурка удалена!" : "Фигурка не найдена.",
                    success ? "Успех" : "Внимание",
                    isError: !success);
            }
            catch (Exception ex)
            {
                _view.ShowMessage(ex.Message, "Ошибка", isError: true);
            }
        }

        /// <summary>
        /// Обрабатывает запрос на показ всех фигурок
        /// </summary>
        private void OnShowAllRequested()
        {
            RefreshFigures();
        }

        /// <summary>
        /// Обрабатывает запрос на группировку по производителям
        /// </summary>
        private void OnGroupByManufacturerRequested()
        {
            var groups = _logic.GroupByManufacturer();
            if (groups.Count == 0)
            {
                _view.ShowMessage("Нет данных для группировки.", "Статистика");
                return;
            }

            var dtoGroups = groups.ToDictionary(
                g => g.Key,
                g => FigureMapper.ToDtoList(g.Value));

            _view.ShowGroups(dtoGroups);
        }

        /// <summary>
        /// Обрабатывает запрос на фильтрацию по масштабу
        /// </summary>
        private void OnFilterByScaleRequested(string scale)
        {
            try
            {
                var filtered = _logic.FilterByScale(scale);
                if (filtered.Count == 0)
                {
                    _view.ShowMessage($"Фигурки с масштабом '{scale}' не найдены.", "Результат");
                    return;
                }

                _view.ShowFigures(FigureMapper.ToDtoList(filtered));
            }
            catch (Exception ex)
            {
                _view.ShowMessage(ex.Message, "Ошибка", isError: true);
            }
        }

        /// <summary>
        /// Обрабатывает выбор фигурки в таблице: заполняет поля ввода
        /// </summary>
        private void OnSelectionChanged(int? id)
        {
            if (id == null) return;

            var figure = _logic.ReadFigure(id.Value);
            if (figure != null)
                _view.FillFields(FigureMapper.ToDto(figure));
        }

        // ===== Обработчик события Model =====

        /// <summary>
        /// Реагирует на изменение данных в Model: перерисовывает View.
        /// Presenter не обновляет таблицу вручную после каждой операции —
        /// он ждёт сигнала от бизнес-логики.
        /// </summary>
        private void OnModelFiguresChanged()
        {
            RefreshFigures();
        }

        /// <summary>
        /// Освобождает ресурсы: отписывается от событий View и Model.
        /// Это предотвращает утечку памяти — View и Presenter перестают удерживать друг друга.
        /// </summary>
        public void Dispose()
        {
            _view.CreateRequested -= OnCreateRequested;
            _view.UpdateRequested -= OnUpdateRequested;
            _view.DeleteRequested -= OnDeleteRequested;
            _view.ShowAllRequested -= OnShowAllRequested;
            _view.GroupByManufacturerRequested -= OnGroupByManufacturerRequested;
            _view.FilterByScaleRequested -= OnFilterByScaleRequested;
            _view.SelectionChanged -= OnSelectionChanged;

            _logic.FiguresChanged -= OnModelFiguresChanged;
        }

        /// <summary>
        /// Загружает все фигурки через фасад и передаёт их во View в виде DTO
        /// </summary>
        private void RefreshFigures()
        {
            var figures = _logic.GetAllFigures();
            _view.ShowFigures(FigureMapper.ToDtoList(figures));
        }
    }
}