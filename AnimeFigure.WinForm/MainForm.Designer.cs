namespace AnimeFigures.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
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
            lblName.Location = new Point(11, 13);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Название:";
            // 
            // lblCharacter
            // 
            lblCharacter.AutoSize = true;
            lblCharacter.Location = new Point(11, 53);
            lblCharacter.Name = "lblCharacter";
            lblCharacter.Size = new Size(84, 20);
            lblCharacter.TabIndex = 1;
            lblCharacter.Text = "Персонаж:";
            // 
            // lblAnime
            // 
            lblAnime.AutoSize = true;
            lblAnime.Location = new Point(11, 93);
            lblAnime.Name = "lblAnime";
            lblAnime.Size = new Size(59, 20);
            lblAnime.TabIndex = 2;
            lblAnime.Text = "Аниме:";
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.Location = new Point(11, 133);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(121, 20);
            lblManufacturer.TabIndex = 3;
            lblManufacturer.Text = "Производитель:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(11, 173);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(48, 20);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "Цена:";
            // 
            // lblScale
            // 
            lblScale.AutoSize = true;
            lblScale.Location = new Point(11, 213);
            lblScale.Name = "lblScale";
            lblScale.Size = new Size(75, 20);
            lblScale.TabIndex = 5;
            lblScale.Text = "Масштаб:";
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(11, 253);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(94, 20);
            lblHeight.TabIndex = 6;
            lblHeight.Text = "Высота (см):";
            // 
            // lblInStock
            // 
            lblInStock.AutoSize = true;
            lblInStock.Location = new Point(11, 293);
            lblInStock.Name = "lblInStock";
            lblInStock.Size = new Size(85, 20);
            lblInStock.TabIndex = 7;
            lblInStock.Text = "В наличии:";
            // 
            // lblRelease
            // 
            lblRelease.AutoSize = true;
            lblRelease.Location = new Point(11, 333);
            lblRelease.Name = "lblRelease";
            lblRelease.Size = new Size(99, 20);
            lblRelease.TabIndex = 8;
            lblRelease.Text = "Дата выхода:";
            // 
            // lblScaleFilter
            // 
            lblScaleFilter.AutoSize = true;
            lblScaleFilter.Location = new Point(11, 413);
            lblScaleFilter.Name = "lblScaleFilter";
            lblScaleFilter.Size = new Size(157, 20);
            lblScaleFilter.TabIndex = 9;
            lblScaleFilter.Text = "Фильтр по масштабу:";
            // 
            // txtName
            // 
            txtName.Location = new Point(171, 13);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(228, 27);
            txtName.TabIndex = 10;
            // 
            // txtCharacter
            // 
            txtCharacter.Location = new Point(171, 53);
            txtCharacter.Margin = new Padding(3, 4, 3, 4);
            txtCharacter.Name = "txtCharacter";
            txtCharacter.Size = new Size(228, 27);
            txtCharacter.TabIndex = 11;
            // 
            // txtAnime
            // 
            txtAnime.Location = new Point(171, 93);
            txtAnime.Margin = new Padding(3, 4, 3, 4);
            txtAnime.Name = "txtAnime";
            txtAnime.Size = new Size(228, 27);
            txtAnime.TabIndex = 12;
            // 
            // txtManufacturer
            // 
            txtManufacturer.Location = new Point(171, 133);
            txtManufacturer.Margin = new Padding(3, 4, 3, 4);
            txtManufacturer.Name = "txtManufacturer";
            txtManufacturer.Size = new Size(228, 27);
            txtManufacturer.TabIndex = 13;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(171, 173);
            txtPrice.Margin = new Padding(3, 4, 3, 4);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(228, 27);
            txtPrice.TabIndex = 14;
            // 
            // txtScale
            // 
            txtScale.Location = new Point(171, 213);
            txtScale.Margin = new Padding(3, 4, 3, 4);
            txtScale.Name = "txtScale";
            txtScale.Size = new Size(228, 27);
            txtScale.TabIndex = 15;
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(171, 253);
            txtHeight.Margin = new Padding(3, 4, 3, 4);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(228, 27);
            txtHeight.TabIndex = 16;
            // 
            // txtInStock
            // 
            txtInStock.Location = new Point(171, 293);
            txtInStock.Margin = new Padding(3, 4, 3, 4);
            txtInStock.Name = "txtInStock";
            txtInStock.Size = new Size(228, 27);
            txtInStock.TabIndex = 17;
            // 
            // txtScaleFilter
            // 
            txtScaleFilter.Location = new Point(171, 413);
            txtScaleFilter.Margin = new Padding(3, 4, 3, 4);
            txtScaleFilter.Name = "txtScaleFilter";
            txtScaleFilter.Size = new Size(228, 27);
            txtScaleFilter.TabIndex = 18;
            // 
            // dtpRelease
            // 
            dtpRelease.Format = DateTimePickerFormat.Short;
            dtpRelease.Location = new Point(171, 333);
            dtpRelease.Margin = new Padding(3, 4, 3, 4);
            dtpRelease.Name = "dtpRelease";
            dtpRelease.Size = new Size(228, 27);
            dtpRelease.TabIndex = 19;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(11, 373);
            btnCreate.Margin = new Padding(3, 4, 3, 4);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(114, 40);
            btnCreate.TabIndex = 20;
            btnCreate.Text = "Добавить";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(137, 373);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(114, 40);
            btnUpdate.TabIndex = 21;
            btnUpdate.Text = "Обновить";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(263, 373);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(114, 40);
            btnDelete.TabIndex = 22;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnGroupByManufacturer
            // 
            btnGroupByManufacturer.Location = new Point(11, 453);
            btnGroupByManufacturer.Margin = new Padding(3, 4, 3, 4);
            btnGroupByManufacturer.Name = "btnGroupByManufacturer";
            btnGroupByManufacturer.Size = new Size(171, 40);
            btnGroupByManufacturer.TabIndex = 23;
            btnGroupByManufacturer.Text = "По производителям";
            btnGroupByManufacturer.UseVisualStyleBackColor = true;
            btnGroupByManufacturer.Click += btnGroupByManufacturer_Click;
            // 
            // btnFilterByScale
            // 
            btnFilterByScale.Location = new Point(194, 453);
            btnFilterByScale.Margin = new Padding(3, 4, 3, 4);
            btnFilterByScale.Name = "btnFilterByScale";
            btnFilterByScale.Size = new Size(114, 40);
            btnFilterByScale.TabIndex = 24;
            btnFilterByScale.Text = "Фильтровать";
            btnFilterByScale.UseVisualStyleBackColor = true;
            btnFilterByScale.Click += btnFilterByScale_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.Location = new Point(320, 453);
            btnShowAll.Margin = new Padding(3, 4, 3, 4);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(114, 40);
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
            dataGridView1.Location = new Point(457, 13);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(857, 933);
            dataGridView1.TabIndex = 26;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1371, 1055);
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
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Магазин аниме-фигурок";
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