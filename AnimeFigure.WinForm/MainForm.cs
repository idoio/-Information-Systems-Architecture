using AnimeFigures.Logic;
using AnimeFigures.Model;
using Ninject;

namespace AnimeFigures.WinForms
{
    public partial class MainForm : Form
    {
        private readonly FigureLogic _logic;
        private BindingSource _bindingSource;
        private readonly IKernel _kernel; // ← Добавляем поле для контейнера

        public MainForm()
        {
            InitializeComponent();

            // 1. Создаем IoC-контейнер с нашим модулем конфигурации
            _kernel = new StandardKernel(new SimpleConfigModule());

            // 2. Получаем экземпляр FigureLogic через контейнер
            // Ninject сам создаст репозиторий и внедрит его в конструктор FigureLogic
            _logic = _kernel.Get<FigureLogic>();

            // 3. Добавляем тестовые данные (если база пустая)
            _logic.AddDefaultFigures();

            InitializeDataGridView();
            LoadFigures();
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
        /// Загружает все фигурки в таблицу
        /// </summary>
        private void LoadFigures()
        {
            _bindingSource.DataSource = _logic.GetAllFigures();
            dataGridView1.Refresh();
        }

        /// <summary>
        /// Возвращает выбранную фигурку или null, если ничего не выбрано
        /// </summary>
        /// <returns>Выбранная фигурка или null</returns>
        private Figure GetSelectedFigure()
        {
            return _bindingSource.Current as Figure;
        }

        /// <summary>
        /// Обработчик кнопки "Добавить": создаёт новую фигурку
        /// </summary>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Цена должна быть неотрицательным числом!", "Ошибка ввода",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!double.TryParse(txtHeight.Text, out double height) || height < 0)
                {
                    MessageBox.Show("Высота должна быть неотрицательным числом!", "Ошибка ввода",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtInStock.Text, out int inStock) || inStock < 0)
                {
                    MessageBox.Show("Количество должно быть неотрицательным целым числом!", "Ошибка ввода",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _logic.CreateFigure(
                    txtName.Text.Trim(),
                    txtCharacter.Text.Trim(),
                    txtAnime.Text.Trim(),
                    txtManufacturer.Text.Trim(),
                    price,
                    txtScale.Text.Trim(),
                    height,
                    dtpRelease.Value,
                    inStock
                );
                LoadFigures();
                ClearFields();
                MessageBox.Show("Фигурка добавлена!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            try
            {
                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Цена должна быть неотрицательным числом!", "Ошибка ввода",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!double.TryParse(txtHeight.Text, out double height) || height < 0)
                {
                    MessageBox.Show("Высота должна быть неотрицательным числом!", "Ошибка ввода",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtInStock.Text, out int inStock) || inStock < 0)
                {
                    MessageBox.Show("Количество должно быть неотрицательным целым числом!", "Ошибка ввода",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _logic.UpdateFigure(
                    selected.Id,
                    txtName.Text.Trim(),
                    txtCharacter.Text.Trim(),
                    txtAnime.Text.Trim(),
                    txtManufacturer.Text.Trim(),
                    price,
                    txtScale.Text.Trim(),
                    height,
                    dtpRelease.Value,
                    inStock
                );
                LoadFigures();
                MessageBox.Show("Данные обновлены!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            {
                if (_logic.DeleteFigure(selected.Id))
                {
                    LoadFigures();
                    MessageBox.Show("Фигурка удалена!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Обработчик кнопки "По производителям": показывает статистику
        /// </summary>
        private void btnGroupByManufacturer_Click(object sender, EventArgs e)
        {
            var groups = _logic.GroupByManufacturer();
            if (groups.Count == 0)
            {
                MessageBox.Show("Нет данных для группировки.", "Статистика",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

            try
            {
                var filtered = _logic.FilterByScale(scale);
                if (filtered.Count == 0)
                {
                    MessageBox.Show($"Фигурки с масштабом '{scale}' не найдены.", "Результат",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _bindingSource.DataSource = filtered;
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Показать все": сбрасывает фильтр и показывает все фигурки
        /// </summary>
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtScaleFilter.Clear();
            LoadFigures();
        }

        /// <summary>
        /// Очищает все поля ввода
        /// </summary>
        private void ClearFields()
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