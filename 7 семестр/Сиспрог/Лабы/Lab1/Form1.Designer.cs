namespace Sys
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
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
            this.dataGrid_supportTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGrid_symbol_table = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbErrorOnePass = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.tbErrorTwoPass = new System.Windows.Forms.TextBox();
            this.tbBinaryCode = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DG_Source)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_OperCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_supportTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_symbol_table)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DG_Source
            // 
            this.DG_Source.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DG_Source.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DG_Source.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_Source.ColumnHeadersVisible = false;
            this.DG_Source.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4 });
            this.DG_Source.Location = new System.Drawing.Point(3, 21);
            this.DG_Source.Margin = new System.Windows.Forms.Padding(0);
            this.DG_Source.Name = "DG_Source";
            this.DG_Source.RowHeadersVisible = false;
            this.DG_Source.RowHeadersWidth = 62;
            this.DG_Source.Size = new System.Drawing.Size(334, 339);
            this.DG_Source.TabIndex = 0;
            this.DG_Source.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGrid_Source_CellBeginEdit);
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
            this.DG_OperCode.ColumnHeadersVisible = false;
            this.DG_OperCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3 });
            this.DG_OperCode.Location = new System.Drawing.Point(3, 392);
            this.DG_OperCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DG_OperCode.Name = "DG_OperCode";
            this.DG_OperCode.RowHeadersVisible = false;
            this.DG_OperCode.RowHeadersWidth = 62;
            this.DG_OperCode.Size = new System.Drawing.Size(334, 339);
            this.DG_OperCode.TabIndex = 1;
            this.DG_OperCode.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGrid_Source_CellBeginEdit);
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
            this.dataGrid_supportTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn5, this.Column7, this.Column8 });
            this.dataGrid_supportTable.Location = new System.Drawing.Point(13, 21);
            this.dataGrid_supportTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGrid_supportTable.Name = "dataGrid_supportTable";
            this.dataGrid_supportTable.ReadOnly = true;
            this.dataGrid_supportTable.RowHeadersVisible = false;
            this.dataGrid_supportTable.RowHeadersWidth = 62;
            this.dataGrid_supportTable.Size = new System.Drawing.Size(407, 269);
            this.dataGrid_supportTable.TabIndex = 2;
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
            // dataGrid_symbol_table
            //
            this.dataGrid_symbol_table.AllowUserToAddRows = false;
            this.dataGrid_symbol_table.AllowUserToDeleteRows = false;
            this.dataGrid_symbol_table.AllowUserToResizeColumns = false;
            this.dataGrid_symbol_table.AllowUserToResizeRows = false;
            this.dataGrid_symbol_table.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_symbol_table.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_symbol_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_symbol_table.ColumnHeadersVisible = false;
            this.dataGrid_symbol_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Column5, this.Column6 });
            this.dataGrid_symbol_table.Location = new System.Drawing.Point(13, 312);
            this.dataGrid_symbol_table.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGrid_symbol_table.Name = "dataGrid_symbol_table";
            this.dataGrid_symbol_table.ReadOnly = true;
            this.dataGrid_symbol_table.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGrid_symbol_table.RowHeadersVisible = false;
            this.dataGrid_symbol_table.RowHeadersWidth = 62;
            this.dataGrid_symbol_table.Size = new System.Drawing.Size(407, 269);
            this.dataGrid_symbol_table.TabIndex = 3;
            //
            // Column5
            //
            this.Column5.HeaderText = "Column5";
            this.Column5.MinimumWidth = 8;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            //
            // Column6
            //
            this.Column6.HeaderText = "Column6";
            this.Column6.MinimumWidth = 8;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Текст";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 371);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Коды операций";
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Вспомогательная таблица";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(205, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Таблица символических имен";
            //
            // button1
            //
            this.button1.Location = new System.Drawing.Point(390, 608);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(407, 53);
            this.button1.TabIndex = 8;
            this.button1.Text = "Первый проход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // button2
            //
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(390, 675);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(407, 53);
            this.button2.TabIndex = 9;
            this.button2.Text = "Второй проход";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            //
            // tbErrorOnePass
            //
            this.tbErrorOnePass.Location = new System.Drawing.Point(830, 412);
            this.tbErrorOnePass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbErrorOnePass.Multiline = true;
            this.tbErrorOnePass.Name = "tbErrorOnePass";
            this.tbErrorOnePass.Size = new System.Drawing.Size(366, 139);
            this.tbErrorOnePass.TabIndex = 10;
            //
            // panel1
            //
            this.panel1.Controls.Add(this.dataGrid_supportTable);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dataGrid_symbol_table);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(377, -1);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 601);
            this.panel1.TabIndex = 11;
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(930, 391);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ошибки первого прохода";
            //
            // panel2
            //
            this.panel2.Controls.Add(this.DG_Source);
            this.panel2.Controls.Add(this.DG_OperCode);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(12, -1);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 741);
            this.panel2.TabIndex = 12;
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(958, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Двоичный код";
            //
            // tbErrorTwoPass
            //
            this.tbErrorTwoPass.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbErrorTwoPass.Location = new System.Drawing.Point(830, 589);
            this.tbErrorTwoPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbErrorTwoPass.Multiline = true;
            this.tbErrorTwoPass.Name = "tbErrorTwoPass";
            this.tbErrorTwoPass.ReadOnly = true;
            this.tbErrorTwoPass.Size = new System.Drawing.Size(366, 139);
            this.tbErrorTwoPass.TabIndex = 6;
            //
            // tbBinaryCode
            //
            this.tbBinaryCode.FormattingEnabled = true;
            this.tbBinaryCode.ItemHeight = 16;
            this.tbBinaryCode.Location = new System.Drawing.Point(830, 20);
            this.tbBinaryCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbBinaryCode.Name = "tbBinaryCode";
            this.tbBinaryCode.Size = new System.Drawing.Size(358, 356);
            this.tbBinaryCode.TabIndex = 13;
            //
            // label6
            //
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(930, 568);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ошибки второго прохода";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1211, 741);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbBinaryCode);
            this.Controls.Add(this.tbErrorTwoPass);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbErrorOnePass);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "1: Двупросмотровый, абсолютный";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DG_Source)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_OperCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_supportTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_symbol_table)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView DG_Source;
        private System.Windows.Forms.DataGridView DG_OperCode;
        private System.Windows.Forms.DataGridView dataGrid_supportTable;
        private System.Windows.Forms.DataGridView dataGrid_symbol_table;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbErrorOnePass;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
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
        private System.Windows.Forms.ListBox tbBinaryCode;
        private System.Windows.Forms.Label label6;
    }
}

