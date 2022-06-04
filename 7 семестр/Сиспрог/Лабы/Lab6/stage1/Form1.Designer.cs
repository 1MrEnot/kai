namespace Lab4
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OperationCodeDataGrid = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceCodeDataGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewTSI = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Link = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewExtDefs = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExtDef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTN = new System.Windows.Forms.DataGridView();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExtRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxFirstErrors = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonStep = new System.Windows.Forms.Button();
            this.textBoxBinCode = new System.Windows.Forms.RichTextBox();
            this.buttonPass = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OperationCodeDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceCodeDataGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTSI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtDefs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTN)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.OperationCodeDataGrid);
            this.groupBox1.Controls.Add(this.SourceCodeDataGrid);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(381, 688);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // OperationCodeDataGrid
            // 
            this.OperationCodeDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.OperationCodeDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.OperationCodeDataGrid.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.OperationCodeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OperationCodeDataGrid.ColumnHeadersVisible = false;
            this.OperationCodeDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Column5, this.Column6, this.Column7 });
            this.OperationCodeDataGrid.Location = new System.Drawing.Point(7, 461);
            this.OperationCodeDataGrid.Name = "OperationCodeDataGrid";
            this.OperationCodeDataGrid.RowHeadersVisible = false;
            this.OperationCodeDataGrid.RowHeadersWidth = 51;
            this.OperationCodeDataGrid.RowTemplate.Height = 24;
            this.OperationCodeDataGrid.Size = new System.Drawing.Size(357, 220);
            this.OperationCodeDataGrid.TabIndex = 9;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Команд";
            this.Column5.MaxInputLength = 20;
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "МКОП";
            this.Column6.MaxInputLength = 20;
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Длина";
            this.Column7.MaxInputLength = 20;
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SourceCodeDataGrid
            // 
            this.SourceCodeDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.SourceCodeDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.SourceCodeDataGrid.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.SourceCodeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SourceCodeDataGrid.ColumnHeadersVisible = false;
            this.SourceCodeDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4 });
            this.SourceCodeDataGrid.Location = new System.Drawing.Point(7, 40);
            this.SourceCodeDataGrid.Name = "SourceCodeDataGrid";
            this.SourceCodeDataGrid.RowHeadersVisible = false;
            this.SourceCodeDataGrid.RowHeadersWidth = 51;
            this.SourceCodeDataGrid.RowTemplate.Height = 24;
            this.SourceCodeDataGrid.Size = new System.Drawing.Size(357, 396);
            this.SourceCodeDataGrid.TabIndex = 8;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Метка";
            this.Column1.MaxInputLength = 20;
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 21;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "МКОП";
            this.Column2.MaxInputLength = 20;
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "Операнд1";
            this.Column3.MaxInputLength = 20;
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4.HeaderText = "Операнд2";
            this.Column4.MaxInputLength = 20;
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 80;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 440);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Коды операций";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Текст";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dataGridViewTSI);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dataGridViewExtDefs);
            this.groupBox2.Controls.Add(this.dataGridViewTN);
            this.groupBox2.Location = new System.Drawing.Point(405, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(469, 688);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // dataGridViewTSI
            // 
            this.dataGridViewTSI.AllowUserToAddRows = false;
            this.dataGridViewTSI.AllowUserToDeleteRows = false;
            this.dataGridViewTSI.AllowUserToResizeColumns = false;
            this.dataGridViewTSI.AllowUserToResizeRows = false;
            this.dataGridViewTSI.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTSI.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewTSI.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridViewTSI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTSI.ColumnHeadersVisible = false;
            this.dataGridViewTSI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.Link, this.Column8, this.Column9 });
            this.dataGridViewTSI.Location = new System.Drawing.Point(19, 40);
            this.dataGridViewTSI.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewTSI.Name = "dataGridViewTSI";
            this.dataGridViewTSI.ReadOnly = true;
            this.dataGridViewTSI.RowHeadersVisible = false;
            this.dataGridViewTSI.RowHeadersWidth = 51;
            this.dataGridViewTSI.Size = new System.Drawing.Size(442, 396);
            this.dataGridViewTSI.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "СИ";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn2.HeaderText = "Адрес";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Link
            // 
            this.Link.DataPropertyName = "Link";
            this.Link.HeaderText = "Ссылки";
            this.Link.MinimumWidth = 6;
            this.Link.Name = "Link";
            this.Link.ReadOnly = true;
            this.Link.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Link.Visible = false;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Type";
            this.Column8.HeaderText = "ВИ";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "section";
            this.Column9.HeaderText = "Секция";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 440);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Внешние имена";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 19);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Таблица символических имен";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 439);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Таблица настройки";
            // 
            // dataGridViewExtDefs
            // 
            this.dataGridViewExtDefs.AllowUserToAddRows = false;
            this.dataGridViewExtDefs.AllowUserToDeleteRows = false;
            this.dataGridViewExtDefs.AllowUserToResizeColumns = false;
            this.dataGridViewExtDefs.AllowUserToResizeRows = false;
            this.dataGridViewExtDefs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewExtDefs.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridViewExtDefs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExtDefs.ColumnHeadersVisible = false;
            this.dataGridViewExtDefs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn3, this.ExtDef });
            this.dataGridViewExtDefs.Location = new System.Drawing.Point(245, 460);
            this.dataGridViewExtDefs.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewExtDefs.MultiSelect = false;
            this.dataGridViewExtDefs.Name = "dataGridViewExtDefs";
            this.dataGridViewExtDefs.ReadOnly = true;
            this.dataGridViewExtDefs.RowHeadersVisible = false;
            this.dataGridViewExtDefs.RowHeadersWidth = 51;
            this.dataGridViewExtDefs.Size = new System.Drawing.Size(216, 220);
            this.dataGridViewExtDefs.TabIndex = 15;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn3.HeaderText = "Адрес";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // ExtDef
            // 
            this.ExtDef.DataPropertyName = "ExtDef";
            this.ExtDef.HeaderText = "Внешнее имя";
            this.ExtDef.MinimumWidth = 6;
            this.ExtDef.Name = "ExtDef";
            this.ExtDef.ReadOnly = true;
            // 
            // dataGridViewTN
            // 
            this.dataGridViewTN.AllowUserToAddRows = false;
            this.dataGridViewTN.AllowUserToDeleteRows = false;
            this.dataGridViewTN.AllowUserToResizeColumns = false;
            this.dataGridViewTN.AllowUserToResizeRows = false;
            this.dataGridViewTN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTN.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridViewTN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTN.ColumnHeadersVisible = false;
            this.dataGridViewTN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Address, this.ExtRef });
            this.dataGridViewTN.Location = new System.Drawing.Point(19, 460);
            this.dataGridViewTN.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewTN.MultiSelect = false;
            this.dataGridViewTN.Name = "dataGridViewTN";
            this.dataGridViewTN.ReadOnly = true;
            this.dataGridViewTN.RowHeadersVisible = false;
            this.dataGridViewTN.RowHeadersWidth = 51;
            this.dataGridViewTN.Size = new System.Drawing.Size(218, 220);
            this.dataGridViewTN.TabIndex = 13;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Адрес";
            this.Address.MinimumWidth = 6;
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // ExtRef
            // 
            this.ExtRef.DataPropertyName = "ExtRef";
            this.ExtRef.HeaderText = "Внешняя ссылка";
            this.ExtRef.MinimumWidth = 6;
            this.ExtRef.Name = "ExtRef";
            this.ExtRef.ReadOnly = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(190, 440);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Ошибки";
            // 
            // textBoxFirstErrors
            // 
            this.textBoxFirstErrors.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxFirstErrors.Location = new System.Drawing.Point(8, 461);
            this.textBoxFirstErrors.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxFirstErrors.Name = "textBoxFirstErrors";
            this.textBoxFirstErrors.ReadOnly = true;
            this.textBoxFirstErrors.Size = new System.Drawing.Size(398, 130);
            this.textBoxFirstErrors.TabIndex = 5;
            this.textBoxFirstErrors.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.buttonReset);
            this.groupBox3.Controls.Add(this.textBoxFirstErrors);
            this.groupBox3.Controls.Add(this.buttonStep);
            this.groupBox3.Controls.Add(this.textBoxBinCode);
            this.groupBox3.Controls.Add(this.buttonPass);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(882, 13);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(418, 688);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 631);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Выбор примера";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "Смешанная адресация", "Относительная адресация", "Прямая адресация" });
            this.comboBox1.Location = new System.Drawing.Point(8, 656);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(275, 24);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(290, 599);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(116, 28);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "Перезапуск";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // buttonStep
            // 
            this.buttonStep.Location = new System.Drawing.Point(150, 599);
            this.buttonStep.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStep.Name = "buttonStep";
            this.buttonStep.Size = new System.Drawing.Size(132, 28);
            this.buttonStep.TabIndex = 5;
            this.buttonStep.Text = "Один шаг";
            this.buttonStep.UseVisualStyleBackColor = true;
            this.buttonStep.Click += new System.EventHandler(this.ButtonStep_Click);
            // 
            // textBoxBinCode
            // 
            this.textBoxBinCode.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxBinCode.Location = new System.Drawing.Point(8, 39);
            this.textBoxBinCode.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBinCode.Name = "textBoxBinCode";
            this.textBoxBinCode.ReadOnly = true;
            this.textBoxBinCode.Size = new System.Drawing.Size(398, 397);
            this.textBoxBinCode.TabIndex = 10;
            this.textBoxBinCode.Text = "";
            // 
            // buttonPass
            // 
            this.buttonPass.Location = new System.Drawing.Point(8, 599);
            this.buttonPass.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPass.Name = "buttonPass";
            this.buttonPass.Size = new System.Drawing.Size(134, 28);
            this.buttonPass.TabIndex = 4;
            this.buttonPass.Text = "Проход";
            this.buttonPass.UseVisualStyleBackColor = true;
            this.buttonPass.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(140, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Двоичный код";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1311, 715);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "6: Однопросмотровый полноперещаемый";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OperationCodeDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceCodeDataGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTSI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtDefs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTN)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label8;

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewTSI;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox textBoxFirstErrors;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonPass;
        private System.Windows.Forms.Button buttonStep;
        private System.Windows.Forms.RichTextBox textBoxBinCode;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.DataGridView OperationCodeDataGrid;
        private System.Windows.Forms.DataGridView SourceCodeDataGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewExtDefs;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExtDef;
        private System.Windows.Forms.DataGridView dataGridViewTN;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExtRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Link;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}

