using AnimeFigures.Presenter;
using AnimeFigures.Shared;

namespace AnimeFigures.WinForms
{
    /// <summary>
    /// Главная форма приложения магазина аниме-фигурок.
    /// Выступает в роли View (MVP): только отрисовывает данные и генерирует события.
    /// Вся бизнес-логика вынесена в MainPresenter.
    /// </summary>
    public partial class MainForm : Form, IMainView
    {
        private BindingSource _bindingSource;
        private MainPresenter _presenter;

        // ===== События View → Presenter =====

        /// <summary>Пользователь запросил добавление фигурки</summary>
        public event Action<FigureDto> CreateRequested;
        /// <summary>Пользователь запросил обновление фигурки</summary>
        public event Action<int, FigureDto> UpdateRequested;
        /// <summary>Пользователь запросил удаление фигурки</summary>
        public event Action<int> DeleteRequested;
        /// <summary>Пользователь запросил показ всех фигурок</summary>
        public event Action ShowAllRequested;
        /// <summary>Пользователь запросил группировку по производителям</summary>
        public event Action GroupByManufacturerRequested;
        /// <summary>Пользователь запросил фильтрацию по масштабу</summary>
        public event Action<string> FilterByScaleRequested;
        /// <summary>Пользователь выбрал фигурку в таблице</summary>
        public event Action<int?> SelectionChanged;

        /// <summary>
        /// Конструктор главной формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeDataGridView();

            // Подписываемся на выбор строки в таблице
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            // Точка сборки создаёт Presenter и внедряет в него эту форму как IMainView.
            // Presenter сам загрузит тестовые данные и вызовет ShowFigures().
            _presenter = CompositionRoot.CreateMainPresenter(this);
        }

        /// <summary>
        /// Инициализирует источник данных для DataGridView
        /// </summary>
        private void InitializeDataGridView()
        {
            _bindingSource = new BindingSource();
            dataGridView1.DataSource = _bindingSource;
        }

        /// <summary>
        /// Возвращает выбранную фигурку или null, если ничего не выбрано
        /// </summary>
        /// <returns>Выбранная фигурка (DTO) или null</returns>
        private FigureDto GetSelectedFigure()
        {
            return _bindingSource.Current as FigureDto;
        }

        /// <summary>
        /// Считывает поля ввода в DTO. Возвращает null, если числовые поля заполнены неверно.
        /// Проверяет только формат ввода (UI-задача), бизнес-валидацию делает Presenter.
        /// </summary>
        private FigureDto ReadFigureFromFields()
        {
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Цена должна быть неотрицательным числом!", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (!double.TryParse(txtHeight.Text, out double height) || height < 0)
            {
                MessageBox.Show("Высота должна быть неотрицательным числом!", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (!int.TryParse(txtInStock.Text, out int inStock) || inStock < 0)
            {
                MessageBox.Show("Количество должно быть неотрицательным целым числом!", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return new FigureDto
            {
                Name = txtName.Text.Trim(),
                Character = txtCharacter.Text.Trim(),
                Anime = txtAnime.Text.Trim(),
                Manufacturer = txtManufacturer.Text.Trim(),
                Price = price,
                Scale = txtScale.Text.Trim(),
                Height = height,
                ReleaseDate = dtpRelease.Value,
                InStock = inStock
            };
        }

        /// <summary>
        /// Обработчик выбора строки в таблице: уведомляет Presenter
        /// </summary>
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var selected = GetSelectedFigure();
            SelectionChanged?.Invoke(selected?.Id);
        }

        /// <summary>
        /// Обработчик кнопки "Добавить": создаёт новую фигурку
        /// </summary>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            var dto = ReadFigureFromFields();
            if (dto != null)
                CreateRequested?.Invoke(dto);
        }

        /// <summary>
        /// Обработчик кнопки "Обновить": обновляет выбранную фигурку
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFigure();
            if (selected == null)
            {
                MessageBox.Show("Выберите фигурку!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = ReadFigureFromFields();
            if (dto != null)
                UpdateRequested?.Invoke(selected.Id, dto);
        }

        /// <summary>
        /// Обработчик кнопки "Удалить": удаляет выбранную фигурку
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFigure();
            if (selected == null)
            {
                MessageBox.Show("Выберите фигурку!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить фигурку \"{selected.Name}\"?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                DeleteRequested?.Invoke(selected.Id);
        }

        /// <summary>
        /// Обработчик кнопки "По производителям": показывает статистику
        /// </summary>
        private void btnGroupByManufacturer_Click(object sender, EventArgs e)
        {
            GroupByManufacturerRequested?.Invoke();
        }

        /// <summary>
        /// Обработчик кнопки "Фильтровать": фильтрует фигурки по масштабу
        /// </summary>
        private void btnFilterByScale_Click(object sender, EventArgs e)
        {
            string scale = txtScaleFilter.Text.Trim();
            if (string.IsNullOrEmpty(scale))
            {
                MessageBox.Show("Введите масштаб для фильтрации!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FilterByScaleRequested?.Invoke(scale);
        }

        /// <summary>
        /// Обработчик кнопки "Показать все": сбрасывает фильтр и показывает все фигурки
        /// </summary>
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtScaleFilter.Clear();
            ShowAllRequested?.Invoke();
        }

        // ===== Реализация IMainView (Presenter → View) =====

        /// <summary>
        /// Отображает список фигурок в таблице
        /// </summary>
        public void ShowFigures(List<FigureDto> figures)
        {
            _bindingSource.DataSource = figures;
            dataGridView1.Refresh();
        }

        /// <summary>
        /// Отображает сообщение пользователю
        /// </summary>
        public void ShowMessage(string message, string title, bool isError = false)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK,
                isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }

        /// <summary>
        /// Отображает результат группировки по производителям
        /// </summary>
        public void ShowGroups(Dictionary<string, List<FigureDto>> groups)
        {
            string message = "Группировка по производителям:\n\n";
            foreach (var group in groups)
            {
                message += $"{group.Key} ({group.Value.Count} шт.):\n";
                foreach (var figure in group.Value)
                    message += $"   • {figure.Name} — {figure.Price} р\n";
                message += "\n";
            }
            MessageBox.Show(message, "Статистика",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Заполняет поля ввода данными выбранной фигурки
        /// </summary>
        public void FillFields(FigureDto figure)
        {
            txtName.Text = figure.Name;
            txtCharacter.Text = figure.Character;
            txtAnime.Text = figure.Anime;
            txtManufacturer.Text = figure.Manufacturer;
            txtPrice.Text = figure.Price.ToString();
            txtScale.Text = figure.Scale;
            txtHeight.Text = figure.Height.ToString();
            txtInStock.Text = figure.InStock.ToString();
            dtpRelease.Value = figure.ReleaseDate;
        }

        /// <summary>
        /// Очищает все поля ввода
        /// </summary>
        public void ClearFields()
        {
            txtName.Clear();
            txtCharacter.Clear();
            txtAnime.Clear();
            txtManufacturer.Clear();
            txtPrice.Clear();
            txtScale.Clear();
            txtHeight.Clear();
            txtInStock.Clear();
            txtScaleFilter.Clear();
            dtpRelease.Value = DateTime.Now;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}