namespace SystemSoftware.Interface
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbSourceCode = new System.Windows.Forms.TextBox();
			this.dgvTmo = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label3 = new System.Windows.Forms.Label();
			this.dgvMacroNames = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label5 = new System.Windows.Forms.Label();
			this.dgvVariables = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label4 = new System.Windows.Forms.Label();
			this.tbAssemblerCode = new System.Windows.Forms.TextBox();
			this.btnRefreshAll = new System.Windows.Forms.Button();
			this.tbError = new System.Windows.Forms.TextBox();
			this.btnFirstRun = new System.Windows.Forms.Button();
			this.btnNextStep = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgvTmo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvMacroNames)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvVariables)).BeginInit();
			this.SuspendLayout();
			// 
			// tbSourceCode
			// 
			this.tbSourceCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbSourceCode.Location = new System.Drawing.Point(10, 26);
			this.tbSourceCode.Margin = new System.Windows.Forms.Padding(4);
			this.tbSourceCode.Multiline = true;
			this.tbSourceCode.Name = "tbSourceCode";
			this.tbSourceCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbSourceCode.Size = new System.Drawing.Size(437, 513);
			this.tbSourceCode.TabIndex = 1;
			this.tbSourceCode.TextChanged += new System.EventHandler(this.OnRefreshAll);
			// 
			// dgvTmo
			// 
			this.dgvTmo.AllowUserToAddRows = false;
			this.dgvTmo.AllowUserToDeleteRows = false;
			this.dgvTmo.AllowUserToResizeColumns = false;
			this.dgvTmo.AllowUserToResizeRows = false;
			this.dgvTmo.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgvTmo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvTmo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTmo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Column1, this.Column2 });
			this.dgvTmo.GridColor = System.Drawing.SystemColors.Window;
			this.dgvTmo.Location = new System.Drawing.Point(457, 26);
			this.dgvTmo.Margin = new System.Windows.Forms.Padding(4);
			this.dgvTmo.Name = "dgvTmo";
			this.dgvTmo.RowHeadersVisible = false;
			this.dgvTmo.RowHeadersWidth = 62;
			this.dgvTmo.Size = new System.Drawing.Size(393, 513);
			this.dgvTmo.TabIndex = 1;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Название";
			this.Column1.MinimumWidth = 8;
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column1.Width = 150;
			// 
			// Column2
			// 
			this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Column2.HeaderText = "Тело";
			this.Column2.MinimumWidth = 8;
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(568, 543);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(168, 17);
			this.label3.TabIndex = 7;
			this.label3.Text = "Таблица имён макросов";
			// 
			// dgvMacroNames
			// 
			this.dgvMacroNames.AllowUserToAddRows = false;
			this.dgvMacroNames.AllowUserToDeleteRows = false;
			this.dgvMacroNames.AllowUserToResizeColumns = false;
			this.dgvMacroNames.AllowUserToResizeRows = false;
			this.dgvMacroNames.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgvMacroNames.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvMacroNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvMacroNames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.Column3 });
			this.dgvMacroNames.GridColor = System.Drawing.SystemColors.Window;
			this.dgvMacroNames.Location = new System.Drawing.Point(458, 564);
			this.dgvMacroNames.Margin = new System.Windows.Forms.Padding(4);
			this.dgvMacroNames.Name = "dgvMacroNames";
			this.dgvMacroNames.RowHeadersVisible = false;
			this.dgvMacroNames.RowHeadersWidth = 62;
			this.dgvMacroNames.Size = new System.Drawing.Size(393, 103);
			this.dgvMacroNames.TabIndex = 6;
			this.dgvMacroNames.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMacroNames_CellContentClick);
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.HeaderText = "Название";
			this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn3.Width = 110;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn4.HeaderText = "Начало";
			this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Конец";
			this.Column3.MinimumWidth = 8;
			this.Column3.Name = "Column3";
			this.Column3.Width = 110;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(173, 543);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(150, 17);
			this.label5.TabIndex = 5;
			this.label5.Text = "Таблица переменных";
			// 
			// dgvVariables
			// 
			this.dgvVariables.AllowUserToAddRows = false;
			this.dgvVariables.AllowUserToDeleteRows = false;
			this.dgvVariables.AllowUserToResizeColumns = false;
			this.dgvVariables.AllowUserToResizeRows = false;
			this.dgvVariables.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgvVariables.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvVariables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2 });
			this.dgvVariables.GridColor = System.Drawing.SystemColors.Window;
			this.dgvVariables.Location = new System.Drawing.Point(10, 564);
			this.dgvVariables.Margin = new System.Windows.Forms.Padding(4);
			this.dgvVariables.Name = "dgvVariables";
			this.dgvVariables.RowHeadersVisible = false;
			this.dgvVariables.RowHeadersWidth = 62;
			this.dgvVariables.Size = new System.Drawing.Size(437, 139);
			this.dgvVariables.TabIndex = 4;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "Название";
			this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn1.Width = 150;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.HeaderText = "Значение";
			this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(568, 5);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(197, 17);
			this.label4.TabIndex = 2;
			this.label4.Text = "Таблица макроопределений";
			// 
			// tbAssemblerCode
			// 
			this.tbAssemblerCode.BackColor = System.Drawing.SystemColors.Window;
			this.tbAssemblerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbAssemblerCode.Location = new System.Drawing.Point(859, 26);
			this.tbAssemblerCode.Margin = new System.Windows.Forms.Padding(4);
			this.tbAssemblerCode.Multiline = true;
			this.tbAssemblerCode.Name = "tbAssemblerCode";
			this.tbAssemblerCode.ReadOnly = true;
			this.tbAssemblerCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbAssemblerCode.Size = new System.Drawing.Size(419, 513);
			this.tbAssemblerCode.TabIndex = 2;
			// 
			// btnRefreshAll
			// 
			this.btnRefreshAll.Enabled = false;
			this.btnRefreshAll.Location = new System.Drawing.Point(730, 675);
			this.btnRefreshAll.Margin = new System.Windows.Forms.Padding(4);
			this.btnRefreshAll.Name = "btnRefreshAll";
			this.btnRefreshAll.Size = new System.Drawing.Size(121, 28);
			this.btnRefreshAll.TabIndex = 55;
			this.btnRefreshAll.Text = "Перезапуск";
			this.btnRefreshAll.UseVisualStyleBackColor = true;
			this.btnRefreshAll.Click += new System.EventHandler(this.OnRefreshAll);
			// 
			// tbError
			// 
			this.tbError.BackColor = System.Drawing.SystemColors.Window;
			this.tbError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbError.Location = new System.Drawing.Point(859, 564);
			this.tbError.Margin = new System.Windows.Forms.Padding(4);
			this.tbError.Multiline = true;
			this.tbError.Name = "tbError";
			this.tbError.ReadOnly = true;
			this.tbError.Size = new System.Drawing.Size(419, 139);
			this.tbError.TabIndex = 42;
			// 
			// btnFirstRun
			// 
			this.btnFirstRun.Enabled = false;
			this.btnFirstRun.Location = new System.Drawing.Point(454, 675);
			this.btnFirstRun.Margin = new System.Windows.Forms.Padding(4);
			this.btnFirstRun.Name = "btnFirstRun";
			this.btnFirstRun.Size = new System.Drawing.Size(139, 28);
			this.btnFirstRun.TabIndex = 54;
			this.btnFirstRun.Text = "Запуск";
			this.btnFirstRun.UseVisualStyleBackColor = true;
			this.btnFirstRun.Click += new System.EventHandler(this.OnFirstRun);
			// 
			// btnNextStep
			// 
			this.btnNextStep.Enabled = false;
			this.btnNextStep.Location = new System.Drawing.Point(601, 675);
			this.btnNextStep.Margin = new System.Windows.Forms.Padding(4);
			this.btnNextStep.Name = "btnNextStep";
			this.btnNextStep.Size = new System.Drawing.Size(120, 28);
			this.btnNextStep.TabIndex = 56;
			this.btnNextStep.Text = "Шаг";
			this.btnNextStep.UseVisualStyleBackColor = true;
			this.btnNextStep.Click += new System.EventHandler(this.OnNextStep);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(1038, 6);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(76, 17);
			this.label6.TabIndex = 9;
			this.label6.Text = "Результат";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(173, 6);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 17);
			this.label1.TabIndex = 84;
			this.label1.Text = "Исходный код";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(1038, 543);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 17);
			this.label2.TabIndex = 85;
			this.label2.Text = "Ошибки";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(1291, 714);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dgvMacroNames);
			this.Controls.Add(this.tbSourceCode);
			this.Controls.Add(this.tbError);
			this.Controls.Add(this.dgvVariables);
			this.Controls.Add(this.dgvTmo);
			this.Controls.Add(this.btnRefreshAll);
			this.Controls.Add(this.btnFirstRun);
			this.Controls.Add(this.btnNextStep);
			this.Controls.Add(this.tbAssemblerCode);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Макропроцессор";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.dgvTmo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvMacroNames)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvVariables)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.Label label2;

		#endregion
		private System.Windows.Forms.TextBox tbSourceCode;
		private System.Windows.Forms.DataGridView dgvTmo;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DataGridView dgvVariables;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbAssemblerCode;
		private System.Windows.Forms.Button btnRefreshAll;
		private System.Windows.Forms.TextBox tbError;
		private System.Windows.Forms.Button btnFirstRun;
		private System.Windows.Forms.Button btnNextStep;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridView dgvMacroNames;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}