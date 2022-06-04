namespace SystemProgramming1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.DG_Source = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DG_OperCode = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DG_SupportTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1Pass = new System.Windows.Forms.Button();
            this.button2Pass = new System.Windows.Forms.Button();
            this.tbErrorOnePass = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DG_TableSetting = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DG_SymbolTable = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.tbErrorTwoPass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxExamples = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbBinaryCode = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.DG_Source)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_OperCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_SupportTable)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_TableSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_SymbolTable)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DG_Source
            // 
            this.DG_Source.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DG_Source.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DG_Source.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_Source.ColumnHeadersVisible = false;
            this.DG_Source.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.DG_Source.Location = new System.Drawing.Point(3, 23);
            this.DG_Source.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DG_Source.Name = "DG_Source";
            this.DG_Source.RowHeadersVisible = false;
            this.DG_Source.RowHeadersWidth = 62;
            this.DG_Source.Size = new System.Drawing.Size(347, 358);
            this.DG_Source.TabIndex = 0;
            this.DG_Source.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGrid_Source_CellBeginEdit);
            this.DG_Source.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DG_Source_KeyUp);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 89.3401F;
            this.Column1.HeaderText = "Column1";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 89.3401F;
            this.Column2.HeaderText = "Column2";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 131.9797F;
            this.Column3.HeaderText = "Column3";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 89.3401F;
            this.Column4.HeaderText = "Column4";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            // 
            // DG_OperCode
            // 
            this.DG_OperCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DG_OperCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_OperCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.DG_OperCode.Location = new System.Drawing.Point(16, 417);
            this.DG_OperCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DG_OperCode.Name = "DG_OperCode";
            this.DG_OperCode.RowHeadersVisible = false;
            this.DG_OperCode.RowHeadersWidth = 62;
            this.DG_OperCode.Size = new System.Drawing.Size(321, 198);
            this.DG_OperCode.TabIndex = 1;
            this.DG_OperCode.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGrid_Source_CellBeginEdit);
            this.DG_OperCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DG_OperCode_KeyUp);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "МКОп";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Код";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Длина";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // DG_SupportTable
            // 
            this.DG_SupportTable.AllowUserToAddRows = false;
            this.DG_SupportTable.AllowUserToDeleteRows = false;
            this.DG_SupportTable.AllowUserToResizeColumns = false;
            this.DG_SupportTable.AllowUserToResizeRows = false;
            this.DG_SupportTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DG_SupportTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DG_SupportTable.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.DG_SupportTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_SupportTable.ColumnHeadersVisible = false;
            this.DG_SupportTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.Column7,
            this.Column8});
            this.DG_SupportTable.Location = new System.Drawing.Point(3, 23);
            this.DG_SupportTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DG_SupportTable.Name = "DG_SupportTable";
            this.DG_SupportTable.ReadOnly = true;
            this.DG_SupportTable.RowHeadersVisible = false;
            this.DG_SupportTable.RowHeadersWidth = 62;
            this.DG_SupportTable.Size = new System.Drawing.Size(566, 243);
            this.DG_SupportTable.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Column5";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Column6";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.MinimumWidth = 8;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.MinimumWidth = 8;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Исходный текст программы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(61, 394);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Таблица кодов операций";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(197, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "Вспомогательная таблица";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 417);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(275, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Таблица символических имен";
            // 
            // button1Pass
            // 
            this.button1Pass.Location = new System.Drawing.Point(80, 655);
            this.button1Pass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1Pass.Name = "button1Pass";
            this.button1Pass.Size = new System.Drawing.Size(167, 39);
            this.button1Pass.TabIndex = 8;
            this.button1Pass.Text = "Первый проход";
            this.button1Pass.UseVisualStyleBackColor = true;
            this.button1Pass.Click += new System.EventHandler(this.button1Pass_Click);
            // 
            // button2Pass
            // 
            this.button2Pass.Enabled = false;
            this.button2Pass.Location = new System.Drawing.Point(577, 655);
            this.button2Pass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2Pass.Name = "button2Pass";
            this.button2Pass.Size = new System.Drawing.Size(167, 39);
            this.button2Pass.TabIndex = 9;
            this.button2Pass.Text = "Второй проход";
            this.button2Pass.UseVisualStyleBackColor = true;
            this.button2Pass.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbErrorOnePass
            // 
            this.tbErrorOnePass.Location = new System.Drawing.Point(135, 304);
            this.tbErrorOnePass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbErrorOnePass.Multiline = true;
            this.tbErrorOnePass.Name = "tbErrorOnePass";
            this.tbErrorOnePass.Size = new System.Drawing.Size(366, 64);
            this.tbErrorOnePass.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DG_TableSetting);
            this.panel1.Controls.Add(this.DG_SymbolTable);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.DG_SupportTable);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbErrorOnePass);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(377, 14);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 627);
            this.panel1.TabIndex = 11;
            // 
            // DG_TableSetting
            // 
            this.DG_TableSetting.AllowUserToAddRows = false;
            this.DG_TableSetting.AllowUserToDeleteRows = false;
            this.DG_TableSetting.AllowUserToResizeColumns = false;
            this.DG_TableSetting.AllowUserToResizeRows = false;
            this.DG_TableSetting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DG_TableSetting.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DG_TableSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_TableSetting.ColumnHeadersVisible = false;
            this.DG_TableSetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.Column11});
            this.DG_TableSetting.Location = new System.Drawing.Point(331, 449);
            this.DG_TableSetting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DG_TableSetting.Name = "DG_TableSetting";
            this.DG_TableSetting.ReadOnly = true;
            this.DG_TableSetting.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DG_TableSetting.RowHeadersVisible = false;
            this.DG_TableSetting.RowHeadersWidth = 62;
            this.DG_TableSetting.Size = new System.Drawing.Size(267, 166);
            this.DG_TableSetting.TabIndex = 15;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Column5";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Column11";
            this.Column11.MinimumWidth = 8;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // DG_SymbolTable
            // 
            this.DG_SymbolTable.AllowUserToAddRows = false;
            this.DG_SymbolTable.AllowUserToDeleteRows = false;
            this.DG_SymbolTable.AllowUserToResizeColumns = false;
            this.DG_SymbolTable.AllowUserToResizeRows = false;
            this.DG_SymbolTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DG_SymbolTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DG_SymbolTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_SymbolTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column9,
            this.Column10});
            this.DG_SymbolTable.Location = new System.Drawing.Point(7, 449);
            this.DG_SymbolTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DG_SymbolTable.Name = "DG_SymbolTable";
            this.DG_SymbolTable.ReadOnly = true;
            this.DG_SymbolTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DG_SymbolTable.RowHeadersVisible = false;
            this.DG_SymbolTable.RowHeadersWidth = 62;
            this.DG_SymbolTable.Size = new System.Drawing.Size(318, 166);
            this.DG_SymbolTable.TabIndex = 14;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Имя";
            this.Column5.MinimumWidth = 8;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Адрес";
            this.Column6.MinimumWidth = 8;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Секция";
            this.Column9.MinimumWidth = 8;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Тип";
            this.Column10.MinimumWidth = 8;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(368, 417);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(182, 21);
            this.label7.TabIndex = 12;
            this.label7.Text = "Таблица настройки";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(197, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(239, 21);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ошибки первого прохода";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DG_Source);
            this.panel2.Controls.Add(this.DG_OperCode);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(16, 14);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 627);
            this.panel2.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(125, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 21);
            this.label8.TabIndex = 12;
            this.label8.Text = "Двоичный код";
            // 
            // tbErrorTwoPass
            // 
            this.tbErrorTwoPass.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbErrorTwoPass.Location = new System.Drawing.Point(3, 553);
            this.tbErrorTwoPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbErrorTwoPass.Multiline = true;
            this.tbErrorTwoPass.Name = "tbErrorTwoPass";
            this.tbErrorTwoPass.ReadOnly = true;
            this.tbErrorTwoPass.Size = new System.Drawing.Size(363, 64);
            this.tbErrorTwoPass.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(95, 530);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(238, 21);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ошибки второго прохода";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(984, 652);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = "Выбор примера";
            // 
            // comboBoxExamples
            // 
            this.comboBoxExamples.FormattingEnabled = true;
            this.comboBoxExamples.Items.AddRange(new object[] {
            "Прямая адресация",
            "Относительная адресация ",
            "Смешанная адресация"});
            this.comboBoxExamples.Location = new System.Drawing.Point(1011, 672);
            this.comboBoxExamples.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxExamples.Name = "comboBoxExamples";
            this.comboBoxExamples.Size = new System.Drawing.Size(343, 24);
            this.comboBoxExamples.TabIndex = 18;
            this.comboBoxExamples.SelectedIndexChanged += new System.EventHandler(this.comboBoxExamples_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tbBinaryCode);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.tbErrorTwoPass);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(987, 14);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(370, 627);
            this.panel3.TabIndex = 13;
            // 
            // tbBinaryCode
            // 
            this.tbBinaryCode.FormattingEnabled = true;
            this.tbBinaryCode.ItemHeight = 16;
            this.tbBinaryCode.Location = new System.Drawing.Point(3, 23);
            this.tbBinaryCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbBinaryCode.Name = "tbBinaryCode";
            this.tbBinaryCode.Size = new System.Drawing.Size(363, 484);
            this.tbBinaryCode.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1283, 675);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.comboBoxExamples);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2Pass);
            this.Controls.Add(this.button1Pass);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Лаба 3: Двухпросмотровый ассемблер в полноперемещаемом формате";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DG_Source)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_OperCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_SupportTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_TableSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_SymbolTable)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DG_Source;
        private System.Windows.Forms.DataGridView DG_OperCode;
        private System.Windows.Forms.DataGridView DG_SupportTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1Pass;
        private System.Windows.Forms.Button button2Pass;
        private System.Windows.Forms.TextBox tbErrorOnePass;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbErrorTwoPass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxExamples;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox tbBinaryCode;
        private System.Windows.Forms.DataGridView DG_SymbolTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridView DG_TableSetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}

