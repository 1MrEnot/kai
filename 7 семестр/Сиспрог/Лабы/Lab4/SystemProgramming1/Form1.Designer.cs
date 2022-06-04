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
            this.dataGrid_Source = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGrid_OperationCode = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGrid_supportTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.первыйПроходToolStripMenuItem = new System.Windows.Forms.Button();
            this.второйПроходToolStripMenuItem = new System.Windows.Forms.Button();
            this.toolStripExamples = new System.Windows.Forms.ComboBox();
            this.toolStripMenuItemSTOP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGrid_symbol_table = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_first_errors = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Source)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_OperationCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_supportTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_symbol_table)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid_Source
            // 
            this.dataGrid_Source.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_Source.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_Source.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Source.ColumnHeadersVisible = false;
            this.dataGrid_Source.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGrid_Source.Location = new System.Drawing.Point(4, 34);
            this.dataGrid_Source.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid_Source.Name = "dataGrid_Source";
            this.dataGrid_Source.RowHeadersVisible = false;
            this.dataGrid_Source.RowHeadersWidth = 62;
            this.dataGrid_Source.Size = new System.Drawing.Size(347, 362);
            this.dataGrid_Source.TabIndex = 0;
            this.dataGrid_Source.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGrid_Source_CellBeginEdit);
            this.dataGrid_Source.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGrid_Source_KeyUp);
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
            // dataGrid_OperationCode
            // 
            this.dataGrid_OperationCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_OperationCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_OperationCode.ColumnHeadersVisible = false;
            this.dataGrid_OperationCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGrid_OperationCode.Location = new System.Drawing.Point(11, 444);
            this.dataGrid_OperationCode.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid_OperationCode.Name = "dataGrid_OperationCode";
            this.dataGrid_OperationCode.RowHeadersVisible = false;
            this.dataGrid_OperationCode.RowHeadersWidth = 62;
            this.dataGrid_OperationCode.Size = new System.Drawing.Size(331, 182);
            this.dataGrid_OperationCode.TabIndex = 1;
            this.dataGrid_OperationCode.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGrid_Source_CellBeginEdit);
            this.dataGrid_OperationCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGrid_OperationCode_KeyUp);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Column3";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGrid_supportTable
            // 
            this.dataGrid_supportTable.AllowUserToAddRows = false;
            this.dataGrid_supportTable.AllowUserToDeleteRows = false;
            this.dataGrid_supportTable.AllowUserToResizeColumns = false;
            this.dataGrid_supportTable.AllowUserToResizeRows = false;
            this.dataGrid_supportTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_supportTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_supportTable.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGrid_supportTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_supportTable.ColumnHeadersVisible = false;
            this.dataGrid_supportTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.Column7,
            this.Column8});
            this.dataGrid_supportTable.Location = new System.Drawing.Point(4, 34);
            this.dataGrid_supportTable.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid_supportTable.Name = "dataGrid_supportTable";
            this.dataGrid_supportTable.ReadOnly = true;
            this.dataGrid_supportTable.RowHeadersVisible = false;
            this.dataGrid_supportTable.RowHeadersWidth = 62;
            this.dataGrid_supportTable.Size = new System.Drawing.Size(441, 592);
            this.dataGrid_supportTable.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 40.60914F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Column5";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 119.797F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Column6";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.FillWeight = 119.797F;
            this.Column7.HeaderText = "Column7";
            this.Column7.MinimumWidth = 8;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.FillWeight = 119.797F;
            this.Column8.HeaderText = "Column8";
            this.Column8.MinimumWidth = 8;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Исходный текст программы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 421);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Таблица кодов операций";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(157, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "Объектный модуль";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGrid_supportTable);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(772, 9);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 630);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGrid_Source);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dataGrid_OperationCode);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(12, 9);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 630);
            this.panel2.TabIndex = 12;
            // 
            // первыйПроходToolStripMenuItem
            // 
            this.первыйПроходToolStripMenuItem.Location = new System.Drawing.Point(12, 665);
            this.первыйПроходToolStripMenuItem.Margin = new System.Windows.Forms.Padding(4);
            this.первыйПроходToolStripMenuItem.Name = "первыйПроходToolStripMenuItem";
            this.первыйПроходToolStripMenuItem.Size = new System.Drawing.Size(196, 48);
            this.первыйПроходToolStripMenuItem.TabIndex = 15;
            this.первыйПроходToolStripMenuItem.Text = "Запуск/продолжить";
            this.первыйПроходToolStripMenuItem.UseVisualStyleBackColor = true;
            this.первыйПроходToolStripMenuItem.Click += new System.EventHandler(this.первыйПроходToolStripMenuItem_Click);
            // 
            // второйПроходToolStripMenuItem
            // 
            this.второйПроходToolStripMenuItem.Location = new System.Drawing.Point(255, 665);
            this.второйПроходToolStripMenuItem.Margin = new System.Windows.Forms.Padding(4);
            this.второйПроходToolStripMenuItem.Name = "второйПроходToolStripMenuItem";
            this.второйПроходToolStripMenuItem.Size = new System.Drawing.Size(196, 48);
            this.второйПроходToolStripMenuItem.TabIndex = 16;
            this.второйПроходToolStripMenuItem.Text = "Один шаг";
            this.второйПроходToolStripMenuItem.UseVisualStyleBackColor = true;
            this.второйПроходToolStripMenuItem.Click += new System.EventHandler(this.второйПроходToolStripMenuItem_Click);
            // 
            // toolStripExamples
            // 
            this.toolStripExamples.FormattingEnabled = true;
            this.toolStripExamples.Items.AddRange(new object[] {
            "Прямая адресация"});
            this.toolStripExamples.Location = new System.Drawing.Point(790, 690);
            this.toolStripExamples.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripExamples.Name = "toolStripExamples";
            this.toolStripExamples.Size = new System.Drawing.Size(426, 24);
            this.toolStripExamples.TabIndex = 17;
            this.toolStripExamples.SelectedIndexChanged += new System.EventHandler(this.toolStripExamples_SelectedIndexChanged);
            // 
            // toolStripMenuItemSTOP
            // 
            this.toolStripMenuItemSTOP.Location = new System.Drawing.Point(498, 665);
            this.toolStripMenuItemSTOP.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripMenuItemSTOP.Name = "toolStripMenuItemSTOP";
            this.toolStripMenuItemSTOP.Size = new System.Drawing.Size(158, 48);
            this.toolStripMenuItemSTOP.TabIndex = 18;
            this.toolStripMenuItemSTOP.Text = "Перезапуск";
            this.toolStripMenuItemSTOP.UseVisualStyleBackColor = true;
            this.toolStripMenuItemSTOP.Click += new System.EventHandler(this.toolStripMenuItemSTOP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(468, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(249, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Таблица символьных имен";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // dataGrid_symbol_table
            // 
            this.dataGrid_symbol_table.AllowUserToAddRows = false;
            this.dataGrid_symbol_table.AllowUserToDeleteRows = false;
            this.dataGrid_symbol_table.AllowUserToResizeColumns = false;
            this.dataGrid_symbol_table.AllowUserToResizeRows = false;
            this.dataGrid_symbol_table.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_symbol_table.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_symbol_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_symbol_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column9});
            this.dataGrid_symbol_table.Location = new System.Drawing.Point(370, 43);
            this.dataGrid_symbol_table.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid_symbol_table.Name = "dataGrid_symbol_table";
            this.dataGrid_symbol_table.ReadOnly = true;
            this.dataGrid_symbol_table.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGrid_symbol_table.RowHeadersVisible = false;
            this.dataGrid_symbol_table.RowHeadersWidth = 62;
            this.dataGrid_symbol_table.Size = new System.Drawing.Size(395, 430);
            this.dataGrid_symbol_table.TabIndex = 3;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Символическое имя";
            this.Column5.MinimumWidth = 8;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Адрес символического имени";
            this.Column6.MinimumWidth = 8;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Значение счетчика адреса";
            this.Column9.MinimumWidth = 8;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // textBox_first_errors
            // 
            this.textBox_first_errors.Location = new System.Drawing.Point(373, 557);
            this.textBox_first_errors.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_first_errors.Multiline = true;
            this.textBox_first_errors.Name = "textBox_first_errors";
            this.textBox_first_errors.Size = new System.Drawing.Size(388, 79);
            this.textBox_first_errors.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(530, 530);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ошибки";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(772, 670);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "Выбор примера";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1223, 741);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_first_errors);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGrid_symbol_table);
            this.Controls.Add(this.toolStripMenuItemSTOP);
            this.Controls.Add(this.toolStripExamples);
            this.Controls.Add(this.второйПроходToolStripMenuItem);
            this.Controls.Add(this.первыйПроходToolStripMenuItem);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Однопросмотровый ассемблер для программы в абсолютном формате";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Source)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_OperationCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_supportTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_symbol_table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid_Source;
        private System.Windows.Forms.DataGridView dataGrid_OperationCode;
        private System.Windows.Forms.DataGridView dataGrid_supportTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Button первыйПроходToolStripMenuItem;
        private System.Windows.Forms.Button второйПроходToolStripMenuItem;
        private System.Windows.Forms.ComboBox toolStripExamples;
        private System.Windows.Forms.Button toolStripMenuItemSTOP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGrid_symbol_table;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.TextBox textBox_first_errors;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
    }
}

