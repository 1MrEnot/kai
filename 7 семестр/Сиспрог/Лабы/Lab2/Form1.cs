namespace Sys
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public int lenRows = 0;
        bool firstPassError = false;
        FirstSecondPass pass;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //При запуске указываем параметр combox по умолчанию 
            comboBoxExamples.SelectedIndex = 0;

            DG_OperCode.Rows.Add("JMP", "01", "4");
            DG_OperCode.Rows.Add("LOADR1", "02", "4");
            DG_OperCode.Rows.Add("LOADR2", "03", "4");
            DG_OperCode.Rows.Add("ADD", "04", "2");
            DG_OperCode.Rows.Add("SAVER1", "05", "4");
            DG_OperCode.Rows.Add("INT", "06", "2");

            DG_Source.Update();
            DG_OperCode.Update();
        }

        void ClearTables()
        {
            dataGrid_supportTable.Rows.Clear();         // очистка ВСПОМОГАТЕЛЬНОЙ таблицы
            dataGrid_symbol_table.Rows.Clear();         // очистка таблицы СИМВОЛЬНЫХ ИМЕН
            dataGrid_TableSetting.Rows.Clear();         // очистка таблицы НАСТРОЕК 
            tbErrorOnePass.Clear();                     // очистка textbox'а ошибка 1-го прохода
            tbErrorTwoPass.Clear();                     // очистка textbox'а ошибка 1-го прохода
            tbBinaryCode.Items.Clear();                 // очистка listbox'а ДВОИЧНЫЙ КОД
        }

        void AddErrorTextBox(TextBox textBox_first_errors, string message)
        {
            textBox_first_errors.Text += message + Environment.NewLine;
        }

        // 1-ый проход
        private void button1Pass_Click(object sender, EventArgs e)
        {
            firstPassError = true;
            pass = new FirstSecondPass();
            ClearTables();
            pass.DeleteEmptyRows(DG_Source);

            //создаем динамический массив куда помещаем нашу таблицу исходного кода
            string[,] arr_SourceCode = new string[DG_Source.RowCount - 1, DG_Source.Columns.Count];

            // заполняем двумерный массив
            for (int i = 0; i < DG_Source.RowCount - 1; i++)
            {
                for (int j = 0; j < DG_Source.Columns.Count; j++)
                    arr_SourceCode[i, j] = Convert.ToString(DG_Source.Rows[i].Cells[j].Value);
            }


            //создаем динамический массив куда помещаем нашу таблицу кодов операций
            string[,] arr_OperCode = new string[DG_OperCode.RowCount - 1, DG_OperCode.Columns.Count];

            for (int i = 0; i < DG_OperCode.RowCount - 1; i++)
            {
                for (int j = 0; j < DG_OperCode.Columns.Count; j++)
                {
                    arr_OperCode[i, j] = Convert.ToString(DG_OperCode.Rows[i].Cells[j].Value);
                    arr_OperCode[i, j] = arr_OperCode[i, j].ToUpper();
                }
            }


            //проверяем таблицу кодов операций
            if (pass.CheckOperationCodeTable(ref arr_OperCode))
            {
                //делаем первый проход
                if (pass.FirstPass(arr_SourceCode, arr_OperCode, dataGrid_supportTable, dataGrid_symbol_table))
                {
                    firstPassError = false;
                    button2.Enabled = true;
                    AddErrorTextBox(tbErrorOnePass, pass.ErrorMessage);
                }
                else
                {
                    AddErrorTextBox(tbErrorOnePass, pass.ErrorMessage);
                }
            }
            else
            {
                AddErrorTextBox(tbErrorOnePass, pass.ErrorMessage);
            }
        }

        // 2-ой проход
        private void button2_Click(object sender, EventArgs e)
        {
            tbBinaryCode.Items.Clear();
            tbErrorTwoPass.Clear();

            if (firstPassError == false)
            {
                //
                if (pass.SecondPass(tbBinaryCode, 2, dataGrid_TableSetting))
                {
                    AddErrorTextBox(tbErrorTwoPass, pass.ErrorMessage);
                }
                else
                {
                    AddErrorTextBox(tbErrorTwoPass, pass.ErrorMessage);
                }
            }
                
            else
                MessageBox.Show("Выполните первый проход");

        }

        private void comboBoxExamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxExamples.SelectedIndex > -1)
            {
                ClearTables();
                button2.Enabled = false;
                firstPassError = true;

                if(comboBoxExamples.SelectedIndex == 0)             // Прямая адресация
                {
                    DG_Source.Rows.Clear();
                    DG_Source.Rows.Add("PROG", "START", "0", "");
                    DG_Source.Rows.Add("", "JMP", "L1", "");
                    DG_Source.Rows.Add("A1", "RESB", "10", "");
                    DG_Source.Rows.Add("A2", "RESW", "20", "");
                    DG_Source.Rows.Add("B1", "WORD", "4096", "");
                    DG_Source.Rows.Add("B2", "BYTE", "X" + '"' + "2F4C008A" + '"', "");
                    DG_Source.Rows.Add("B3", "BYTE", "C" + '"' + "Hello!" + '"', "");
                    DG_Source.Rows.Add("B4", "BYTE", "128", "");
                    DG_Source.Rows.Add("L1", "LOADR1", "B1", "");
                    DG_Source.Rows.Add("", "LOADR2", "B4", "");
                    DG_Source.Rows.Add("", "ADD", "R1", "R2");
                    DG_Source.Rows.Add("", "SAVER1", "L1", "");
                    DG_Source.Rows.Add("", "INT", "200", "");
                    DG_Source.Rows.Add("", "END", "10", "");
                }
                else if (comboBoxExamples.SelectedIndex == 1)        // Относительная адресация 
                {
                    DG_Source.Rows.Clear();
                    DG_Source.Rows.Add("PROG", "START", "0", "");
                    DG_Source.Rows.Add("", "JMP", "[L1]", "");
                    DG_Source.Rows.Add("A1", "RESB", "10", "");
                    DG_Source.Rows.Add("A2", "RESW", "20", "");
                    DG_Source.Rows.Add("B1", "WORD", "4096", "");
                    DG_Source.Rows.Add("B2", "BYTE", "X" + '"' + "2F4C008A" + '"', "");
                    DG_Source.Rows.Add("B3", "BYTE", "C" + '"' + "Hello!" + '"', "");
                    DG_Source.Rows.Add("B4", "BYTE", "128", "");
                    DG_Source.Rows.Add("L1", "LOADR1", "[B1]", "");
                    DG_Source.Rows.Add("", "LOADR2", "[B4]", "");
                    DG_Source.Rows.Add("", "ADD", "R1", "R2");
                    DG_Source.Rows.Add("", "SAVER1", "[L1]", "");
                    DG_Source.Rows.Add("", "INT", "200", "");
                    DG_Source.Rows.Add("", "END", "10", "");
                }
                else if (comboBoxExamples.SelectedIndex == 2)         // Смешанная адресация
                {
                    DG_Source.Rows.Clear();
                    DG_Source.Rows.Add("PROG", "START", "0", "");
                    DG_Source.Rows.Add("", "JMP", "[L1]", "");
                    DG_Source.Rows.Add("A1", "RESB", "10", "");
                    DG_Source.Rows.Add("A2", "RESW", "20", "");
                    DG_Source.Rows.Add("B1", "WORD", "4096", "");
                    DG_Source.Rows.Add("B2", "BYTE", "X" + '"' + "2F4C008A" + '"', "");
                    DG_Source.Rows.Add("B3", "BYTE", "C" + '"' + "Hello!" + '"', "");
                    DG_Source.Rows.Add("B4", "BYTE", "128", "");
                    DG_Source.Rows.Add("L1", "LOADR1", "B1", "");
                    DG_Source.Rows.Add("", "LOADR2", "[B4]", "");
                    DG_Source.Rows.Add("", "ADD", "R1", "R2");
                    DG_Source.Rows.Add("", "SAVER1", "[L1]", "");
                    DG_Source.Rows.Add("", "INT", "200", "");
                    DG_Source.Rows.Add("", "END", "10", "");
                }
            }
        }

        private void dataGrid_Source_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            ClearTables();
            button2.Enabled = false;
        }

        // обработка нажатий на кнопку Enter,Backspace в dataGridView_SourceCode
        private void DG_Source_KeyUp(object sender, KeyEventArgs e)
        {
            int selectedInd = DG_Source.CurrentRow.Index;

            if (e.KeyCode == Keys.Enter)         //добавление новой строки в DG_Source при нажатии на кнопку Enter
                DG_Source.Rows.Insert(selectedInd, "", "", "", "");
            //else if(e.KeyCode == Keys.Back)      //удаление пустой строки в DG_Source при нажатии на кнопку Backspace
            //{
            //    for(int i = DG_Source.Rows.Count - 1; i > -1; i--)
            //    {
            //        DataGridViewRow row = DG_Source.Rows[i];

            //        for(int j = 0; j < DG_Source.ColumnCount; j++)
            //        {
            //            if (!row.IsNewRow && row.Cells[j].Value == null)
            //                DG_Source.Rows.Remove(row);
            //        }
            //    }
            //}
        }

        // обработка нажатий на кнопку Enter,Backspace в dataGridView_OperCodeTable
        private void DG_OperCode_KeyUp(object sender, KeyEventArgs e)
        {
            int selectedInd = DG_OperCode.CurrentRow.Index;

            if (e.KeyCode == Keys.Enter)         //добавление новой строки в DG_Source при нажатии на кнопку Enter
                DG_OperCode.Rows.Insert(selectedInd, "", "", "", "");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
