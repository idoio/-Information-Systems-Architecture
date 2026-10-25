namespace AnimeFigures.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                _kernel?.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblName = new Label();
            lblCharacter = new Label();
            lblAnime = new Label();
            lblManufacturer = new Label();
            lblPrice = new Label();
            lblScale = new Label();
            lblHeight = new Label();
            lblInStock = new Label();
            lblRelease = new Label();
            lblScaleFilter = new Label();
            txtName = new TextBox();
            txtCharacter = new TextBox();
            txtAnime = new TextBox();
            txtManufacturer = new TextBox();
            txtPrice = new TextBox();
            txtScale = new TextBox();
            txtHeight = new TextBox();
            txtInStock = new TextBox();
            txtScaleFilter = new TextBox();
            dtpRelease = new DateTimePicker();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnGroupByManufacturer = new Button();
            btnFilterByScale = new Button();
            btnShowAll = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 10);
            lblName.Name = "lblName";
            lblName.Size = new Size(62, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Название:";
            // 
            // lblCharacter
            // 
            lblCharacter.AutoSize = true;
            lblCharacter.Location = new Point(10, 40);
            lblCharacter.Name = "lblCharacter";
            lblCharacter.Size = new Size(67, 15);
            lblCharacter.TabIndex = 1;
            lblCharacter.Text = "Персонаж:";
            // 
            // lblAnime
            // 
            lblAnime.AutoSize = true;
            lblAnime.Location = new Point(10, 70);
            lblAnime.Name = "lblAnime";
            lblAnime.Size = new Size(47, 15);
            lblAnime.TabIndex = 2;
            lblAnime.Text = "Аниме:";
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.Location = new Point(10, 100);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(95, 15);
            lblManufacturer.TabIndex = 3;
            lblManufacturer.Text = "Производитель:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(10, 130);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(38, 15);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "Цена:";
            // 
            // lblScale
            // 
            lblScale.AutoSize = true;
            lblScale.Location = new Point(10, 160);
            lblScale.Name = "lblScale";
            lblScale.Size = new Size(62, 15);
            lblScale.TabIndex = 5;
            lblScale.Text = "Масштаб:";
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(10, 190);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(76, 15);
            lblHeight.TabIndex = 6;
            lblHeight.Text = "Высота (см):";
            // 
            // lblInStock
            // 
            lblInStock.AutoSize = true;
            lblInStock.Location = new Point(10, 220);
            lblInStock.Name = "lblInStock";
            lblInStock.Size = new Size(68, 15);
            lblInStock.TabIndex = 7;
            lblInStock.Text = "В наличии:";
            // 
            // lblRelease
            // 
            lblRelease.AutoSize = true;
            lblRelease.Location = new Point(10, 250);
            lblRelease.Name = "lblRelease";
            lblRelease.Size = new Size(78, 15);
            lblRelease.TabIndex = 8;
            lblRelease.Text = "Дата выхода:";
            // 
            // lblScaleFilter
            // 
            lblScaleFilter.AutoSize = true;
            lblScaleFilter.Location = new Point(10, 310);
            lblScaleFilter.Name = "lblScaleFilter";
            lblScaleFilter.Size = new Size(127, 15);
            lblScaleFilter.TabIndex = 9;
            lblScaleFilter.Text = "Фильтр по масштабу:";
            // 
            // txtName
            // 
            txtName.Location = new Point(150, 10);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 10;
            // 
            // txtCharacter
            // 
            txtCharacter.Location = new Point(150, 40);
            txtCharacter.Name = "txtCharacter";
            txtCharacter.Size = new Size(200, 23);
            txtCharacter.TabIndex = 11;
            // 
            // txtAnime
            // 
            txtAnime.Location = new Point(150, 70);
            txtAnime.Name = "txtAnime";
            txtAnime.Size = new Size(200, 23);
            txtAnime.TabIndex = 12;
            // 
            // txtManufacturer
            // 
            txtManufacturer.Location = new Point(150, 100);
            txtManufacturer.Name = "txtManufacturer";
            txtManufacturer.Size = new Size(200, 23);
            txtManufacturer.TabIndex = 13;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(150, 130);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(200, 23);
            txtPrice.TabIndex = 14;
            // 
            // txtScale
            // 
            txtScale.Location = new Point(150, 160);
            txtScale.Name = "txtScale";
            txtScale.Size = new Size(200, 23);
            txtScale.TabIndex = 15;
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(150, 190);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(200, 23);
            txtHeight.TabIndex = 16;
            // 
            // txtInStock
            // 
            txtInStock.Location = new Point(150, 220);
            txtInStock.Name = "txtInStock";
            txtInStock.Size = new Size(200, 23);
            txtInStock.TabIndex = 17;
            // 
            // txtScaleFilter
            // 
            txtScaleFilter.Location = new Point(150, 310);
            txtScaleFilter.Name = "txtScaleFilter";
            txtScaleFilter.Size = new Size(200, 23);
            txtScaleFilter.TabIndex = 18;
            // 
            // dtpRelease
            // 
            dtpRelease.Format = DateTimePickerFormat.Short;
            dtpRelease.Location = new Point(150, 250);
            dtpRelease.Name = "dtpRelease";
            dtpRelease.Size = new Size(200, 23);
            dtpRelease.TabIndex = 19;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(10, 280);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(100, 30);
            btnCreate.TabIndex = 20;
            btnCreate.Text = "Добавить";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(120, 280);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 30);
            btnUpdate.TabIndex = 21;
            btnUpdate.Text = "Обновить";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(230, 280);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 30);
            btnDelete.TabIndex = 22;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnGroupByManufacturer
            // 
            btnGroupByManufacturer.Location = new Point(10, 340);
            btnGroupByManufacturer.Name = "btnGroupByManufacturer";
            btnGroupByManufacturer.Size = new Size(150, 30);
            btnGroupByManufacturer.TabIndex = 23;
            btnGroupByManufacturer.Text = "По производителям";
            btnGroupByManufacturer.UseVisualStyleBackColor = true;
            btnGroupByManufacturer.Click += btnGroupByManufacturer_Click;
            // 
            // btnFilterByScale
            // 
            btnFilterByScale.Location = new Point(170, 340);
            btnFilterByScale.Name = "btnFilterByScale";
            btnFilterByScale.Size = new Size(100, 30);
            btnFilterByScale.TabIndex = 24;
            btnFilterByScale.Text = "Фильтровать";
            btnFilterByScale.UseVisualStyleBackColor = true;
            btnFilterByScale.Click += btnFilterByScale_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.Location = new Point(280, 340);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(100, 30);
            btnShowAll.TabIndex = 25;
            btnShowAll.Text = "Показать все";
            btnShowAll.UseVisualStyleBackColor = true;
            btnShowAll.Click += btnShowAll_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(400, 10);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(750, 700);
            dataGridView1.TabIndex = 26;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 791);
            Controls.Add(dataGridView1);
            Controls.Add(btnShowAll);
            Controls.Add(btnFilterByScale);
            Controls.Add(btnGroupByManufacturer);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnCreate);
            Controls.Add(dtpRelease);
            Controls.Add(txtScaleFilter);
            Controls.Add(txtInStock);
            Controls.Add(txtHeight);
            Controls.Add(txtScale);
            Controls.Add(txtPrice);
            Controls.Add(txtManufacturer);
            Controls.Add(txtAnime);
            Controls.Add(txtCharacter);
            Controls.Add(txtName);
            Controls.Add(lblScaleFilter);
            Controls.Add(lblRelease);
            Controls.Add(lblInStock);
            Controls.Add(lblHeight);
            Controls.Add(lblScale);
            Controls.Add(lblPrice);
            Controls.Add(lblManufacturer);
            Controls.Add(lblAnime);
            Controls.Add(lblCharacter);
            Controls.Add(lblName);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Магазин аниме-фигурок";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCharacter;
        private System.Windows.Forms.Label lblAnime;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblInStock;
        private System.Windows.Forms.Label lblRelease;
        private System.Windows.Forms.Label lblScaleFilter;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCharacter;
        private System.Windows.Forms.TextBox txtAnime;
        private System.Windows.Forms.TextBox txtManufacturer;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtInStock;
        private System.Windows.Forms.TextBox txtScaleFilter;
        private System.Windows.Forms.DateTimePicker dtpRelease;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnGroupByManufacturer;
        private System.Windows.Forms.Button btnFilterByScale;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}