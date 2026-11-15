namespace AnimeFigures.Shared
{
    /// <summary>
    /// Интерфейс главного представления (View).
    /// Реализуется формой MainForm. Presenter работает с View только через этот интерфейс.
    /// </summary>
    public interface IMainView
    {
        // ===== События: View → Presenter =====
        // Форма только сообщает "пользователь что-то сделал", не выполняя логику

        /// <summary>Пользователь запросил добавление фигурки (данные формы в FigureDto)</summary>
        event Action<FigureDto> CreateRequested;

        /// <summary>Пользователь запросил обновление фигурки с указанным Id</summary>
        event Action<int, FigureDto> UpdateRequested;

        /// <summary>Пользователь запросил удаление фигурки с указанным Id</summary>
        event Action<int> DeleteRequested;

        /// <summary>Пользователь запросил показ всех фигурок</summary>
        event Action ShowAllRequested;

        /// <summary>Пользователь запросил группировку по производителям</summary>
        event Action GroupByManufacturerRequested;

        /// <summary>Пользователь запросил фильтрацию по масштабу</summary>
        event Action<string> FilterByScaleRequested;

        /// <summary>Пользователь выбрал фигурку в таблице (Id или null)</summary>
        event Action<int?> SelectionChanged;

        // ===== Методы: Presenter → View =====
        // Presenter вызывает их для отрисовки данных

        /// <summary>Отображает список фигурок в таблице</summary>
        void ShowFigures(List<FigureDto> figures);

        /// <summary>Отображает сообщение пользователю</summary>
        void ShowMessage(string message, string title, bool isError = false);

        /// <summary>Отображает результат группировки по производителям</summary>
        void ShowGroups(Dictionary<string, List<FigureDto>> groups);

        /// <summary>Заполняет поля ввода данными выбранной фигурки</summary>
        void FillFields(FigureDto figure);

        /// <summary>Очищает поля ввода</summary>
        void ClearFields();
    }
}