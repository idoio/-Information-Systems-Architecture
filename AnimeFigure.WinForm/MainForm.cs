using AnimeFigures.Logic;
using AnimeFigures.Model;

namespace AnimeFigures.WinForms
{
    public partial class MainForm : Form
    {
        private readonly FigureLogic _logic;
        private BindingSource _bindingSource;

        // Элементы управления
        private DataGridView dataGridView1;
        private TextBox txtName, txtCharacter, txtAnime, txtManufacturer, txtPrice, txtScale, txtHeight, txtInStock, txtScaleFilter;
        private DateTimePicker dtpRelease;
        private Button btnCreate, btnUpdate, btnDelete, btnGroupByManufacturer, btnFilterByScale, btnShowAll;
        private Label lblName, lblCharacter, lblAnime, lblManufacturer, lblPrice, lblScale, lblHeight, lblInStock, lblRelease, lblScaleFilter;

        public MainForm()
        {
            InitializeCustomComponents();
            _logic = new FigureLogic();
            InitializeDataGridView();
            LoadFigures();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Магазин аниме-фигурок";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Создаем метки
            lblName = CreateLabel("Название:", 10, 10);
            lblCharacter = CreateLabel("Персонаж:", 10, 40);
            lblAnime = CreateLabel("Аниме:", 10, 70);
            lblManufacturer = CreateLabel("Производитель:", 10, 100);
            lblPrice = CreateLabel("Цена:", 10, 130);
            lblScale = CreateLabel("Масштаб:", 10, 160);
            lblHeight = CreateLabel("Высота (см):", 10, 190);
            lblInStock = CreateLabel("В наличии:", 10, 220);
            lblRelease = CreateLabel("Дата выхода:", 10, 250);
            lblScaleFilter = CreateLabel("Фильтр по масштабу:", 10, 310);

            // Создаем текстовые поля
            txtName = CreateTextBox(150, 10);
            txtCharacter = CreateTextBox(150, 40);
            txtAnime = CreateTextBox(150, 70);
            txtManufacturer = CreateTextBox(150, 100);
            txtPrice = CreateTextBox(150, 130);
            txtScale = CreateTextBox(150, 160);
            txtHeight = CreateTextBox(150, 190);
            txtInStock = CreateTextBox(150, 220);
            txtScaleFilter = CreateTextBox(150, 310);

            // Создаем DatePicker
            dtpRelease = new DateTimePicker();
            dtpRelease.Location = new Point(150, 250);
            dtpRelease.Size = new Size(200, 20);
            this.Controls.Add(dtpRelease);

            // Создаем кнопки
            btnCreate = CreateButton("Добавить", 10, 280, 100, 30);
            btnUpdate = CreateButton("Обновить", 120, 280, 100, 30);
            btnDelete = CreateButton("Удалить", 230, 280, 100, 30);
            btnGroupByManufacturer = CreateButton("По производителям", 10, 340, 150, 30);
            btnFilterByScale = CreateButton("Фильтровать", 170, 340, 100, 30);
            btnShowAll = CreateButton("Показать все", 280, 340, 100, 30);

            // Добавляем обработчики событий
            btnCreate.Click += btnCreate_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnGroupByManufacturer.Click += btnGroupByManufacturer_Click;
            btnFilterByScale.Click += btnFilterByScale_Click;
            btnShowAll.Click += btnShowAll_Click;

            // Добавляем все элементы на форму
            this.Controls.AddRange(new Control[] {
                lblName, lblCharacter, lblAnime, lblManufacturer, lblPrice, lblScale, lblHeight, lblInStock, lblRelease, lblScaleFilter,
                txtName, txtCharacter, txtAnime, txtManufacturer, txtPrice, txtScale, txtHeight, txtInStock, txtScaleFilter,
                btnCreate, btnUpdate, btnDelete, btnGroupByManufacturer, btnFilterByScale, btnShowAll
            });
        }

        private Label CreateLabel(string text, int x, int y)
        {
            var label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.AutoSize = true;
            this.Controls.Add(label);
            return label;
        }

        private TextBox CreateTextBox(int x, int y)
        {
            var textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(200, 20);
            this.Controls.Add(textBox);
            return textBox;
        }

        private Button CreateButton(string text, int x, int y, int width, int height)
        {
            var button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            this.Controls.Add(button);
            return button;
        }

        private void InitializeDataGridView()
        {
            dataGridView1 = new DataGridView();
            dataGridView1.Location = new Point(400, 10);
            dataGridView1.Size = new Size(750, 700);
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _bindingSource = new BindingSource();
            dataGridView1.DataSource = _bindingSource;

            this.Controls.Add(dataGridView1);
        }

        private void LoadFigures()
        {
            _bindingSource.DataSource = _logic.GetAllFigures();
            dataGridView1.Refresh();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                _logic.CreateFigure(
                    txtName.Text.Trim(),
                    txtCharacter.Text.Trim(),
                    txtAnime.Text.Trim(),
                    txtManufacturer.Text.Trim(),
                    decimal.Parse(txtPrice.Text),
                    txtScale.Text.Trim(),
                    double.Parse(txtHeight.Text),
                    dtpRelease.Value,
                    int.Parse(txtInStock.Text)
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите фигурку!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = ((Figure)dataGridView1.SelectedRows[0].DataBoundItem).Id;
                _logic.UpdateFigure(
                    id,
                    txtName.Text.Trim(),
                    txtCharacter.Text.Trim(),
                    txtAnime.Text.Trim(),
                    txtManufacturer.Text.Trim(),
                    decimal.Parse(txtPrice.Text),
                    txtScale.Text.Trim(),
                    double.Parse(txtHeight.Text),
                    int.Parse(txtInStock.Text)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите фигурку!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Удалить выбранную фигурку?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int id = ((Figure)dataGridView1.SelectedRows[0].DataBoundItem).Id;
                if (_logic.DeleteFigure(id))
                {
                    LoadFigures();
                    MessageBox.Show("Фигурка удалена!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnGroupByManufacturer_Click(object sender, EventArgs e)
        {
            var groups = _logic.GroupByManufacturer();
            string message = "?? Группировка по производителям:\n\n";
            foreach (var group in groups)
            {
                message += $"{group.Key}: {group.Value} шт.\n";
            }
            MessageBox.Show(message, "Статистика",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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
                _bindingSource.DataSource = filtered;
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadFigures();
        }

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
    }
}