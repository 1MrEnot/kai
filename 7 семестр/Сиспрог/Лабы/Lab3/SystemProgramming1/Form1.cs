using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SystemProgramming1
{
    public partial class Form1 : Form
    {

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
            DG_SupportTable.Rows.Clear();
            DG_SymbolTable.Rows.Clear();
            DG_TableSetting.Rows.Clear();
            tbErrorOnePass.Clear();
            tbErrorTwoPass.Clear();
            tbBinaryCode.Items.Clear();
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
            pass.DeleteEmptyRows(DG_OperCode);

            //создаем динамический массив куда помещаем нашу таблицу исходного кода
            string[,] arr_SourceCode = new string[DG_Source.RowCount - 1, DG_Source.Columns.Count];

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
                if (pass.FirstPass(arr_SourceCode, arr_OperCode, DG_SupportTable, DG_SymbolTable))
                {
                    firstPassError = false;
                    button2Pass.Enabled = true;
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
                if (pass.SecondPass(tbBinaryCode, 2, DG_TableSetting))
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
                button2Pass.Enabled = false;
                firstPassError = true;

                if (comboBoxExamples.SelectedIndex == 0)
                {
                    DG_Source.Rows.Clear();

                    DG_Source.Rows.Add("PROG", "START", "0", "");
                    DG_Source.Rows.Add("", "EXTDEF", "D23", "");
                    DG_Source.Rows.Add("", "EXTDEF", "D4", "");
                    DG_Source.Rows.Add("", "EXTREF", "D2", "");
                    DG_Source.Rows.Add("", "EXTREF", "D546", "");
                    DG_Source.Rows.Add("D4", "RESB", "10", "");
                    DG_Source.Rows.Add("D23", "RESB", "10", "");
                    DG_Source.Rows.Add("", "JMP", "D2", "");
                    DG_Source.Rows.Add("", "SAVER1", "D546", "");
                    DG_Source.Rows.Add("", "RESB", "10", "");
                    //dataGrid_Source.Rows.Add("", "", "", "");
                    DG_Source.Rows.Add("A2", "CSECT", "", "");
                    DG_Source.Rows.Add("", "EXTDEF", "D42", "");
                    DG_Source.Rows.Add("", "EXTREF", "D4", "");
                    //dataGrid_Source.Rows.Add("", "EXTREF", "D58", "");
                    DG_Source.Rows.Add("D42", "SAVER1", "D4", "");
                    DG_Source.Rows.Add("", "INT", "200", "");
                    DG_Source.Rows.Add("", "END", "0", "");
                }
                if (comboBoxExamples.SelectedIndex == 1)
                {
                    DG_Source.Rows.Clear();

                    DG_Source.Rows.Add("PROG", "START", "0", "");
                    DG_Source.Rows.Add("", "EXTDEF", "D23", "");
                    DG_Source.Rows.Add("", "EXTDEF", "D4", "");
                    DG_Source.Rows.Add("", "EXTREF", "D2", "");
                    DG_Source.Rows.Add("", "EXTREF", "D546", "");
                    DG_Source.Rows.Add("D4", "RESB", "10", "");
                    DG_Source.Rows.Add("D23", "RESB", "10", "");
                    DG_Source.Rows.Add("T1", "JMP", "D2", "");
                    DG_Source.Rows.Add("", "SAVER1", "D546", "");
                    DG_Source.Rows.Add("D42", "LOADR2", "[T1]", "");
                    DG_Source.Rows.Add("", "RESB", "10", "");
                    //dataGrid_Source.Rows.Add("", "", "", "");
                    DG_Source.Rows.Add("A2", "CSECT", "", "");
                    DG_Source.Rows.Add("", "EXTDEF", "D2", "");
                    DG_Source.Rows.Add("", "EXTREF", "D4", "");
                    DG_Source.Rows.Add("", "EXTREF", "D58", "");
                    DG_Source.Rows.Add("D2", "SAVER1", "[D2]", "");
                    DG_Source.Rows.Add("", "LOADR1", "[D2]", "");
                    DG_Source.Rows.Add("T3", "INT", "200", "");
                    DG_Source.Rows.Add("", "END", "0", "");
                }
                if (comboBoxExamples.SelectedIndex == 2)
                {
                    DG_Source.Rows.Clear();

                    DG_Source.Rows.Add("PROG", "START", "0", "");
                    DG_Source.Rows.Add("", "EXTDEF", "D23", "");
                    DG_Source.Rows.Add("", "EXTDEF", "D4", "");
                    DG_Source.Rows.Add("", "EXTREF", "D2", "");
                    DG_Source.Rows.Add("", "EXTREF", "D546", "");
                    DG_Source.Rows.Add("D4", "RESB", "10", "");
                    DG_Source.Rows.Add("D23", "RESB", "10", "");
                    DG_Source.Rows.Add("T1", "JMP", "D2", "");
                    DG_Source.Rows.Add("", "SAVER1", "D546", "");
                    DG_Source.Rows.Add("D42", "LOADR2", "[T1]", "");
                    DG_Source.Rows.Add("", "RESB", "10", ""); ;
                    DG_Source.Rows.Add("", "", "", "");
                    DG_Source.Rows.Add("A2", "CSECT", "", "");
                    DG_Source.Rows.Add("", "EXTDEF", "D2", "");
                    DG_Source.Rows.Add("", "EXTREF", "D4", "");
                    //dataGrid_Source.Rows.Add("", "EXTREF", "D58", "");
                    DG_Source.Rows.Add("D2", "SAVER1", "[D2]", "");
                    DG_Source.Rows.Add("", "LOADR1", "[D2]", "");
                    DG_Source.Rows.Add("T3", "INT", "200", "");
                    DG_Source.Rows.Add("", "END", "0", "");
                }                
            }
        }

        private void dataGrid_Source_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            ClearTables();
            button2Pass.Enabled = false;
        }

        // обработка нажатий на кнопку Enter,Backspace в dataGridView_SourceCode
        private void DG_Source_KeyUp(object sender, KeyEventArgs e)
        {
            int selectedInd = DG_Source.CurrentRow.Index;

            if (e.KeyCode == Keys.Enter)         //добавление новой строки в DG_Source при нажатии на кнопку Enter
                DG_Source.Rows.Insert(selectedInd, "", "", "", "");
            //else if (e.KeyCode == Keys.Back)      //удаление пустой строки в DG_Source при нажатии на кнопку Backspace
            //{
            //    for (int i = DG_Source.Rows.Count - 1; i > -1; i--)
            //    {
            //        DataGridViewRow row = DG_Source.Rows[i];

            //        for (int j = 0; j < DG_Source.ColumnCount; j++)
            //        {
            //            if (!row.IsNewRow && row.Cells[j].Value == null)
            //                DG_Source.Rows.RemoveAt(i);
            //        }
            //    }
            //}
        }

        private void DG_OperCode_KeyUp(object sender, KeyEventArgs e)
        {
            int selectedInd = DG_OperCode.CurrentRow.Index;

            if (e.KeyCode == Keys.Enter)         //добавление новой строки в DG_Source при нажатии на кнопку Enter
                DG_OperCode.Rows.Insert(selectedInd, "", "", "", "");
            //else if (e.KeyCode == Keys.Back)      //удаление пустой строки в DG_Source при нажатии на кнопку Backspace
            //{
            //    for (int i = DG_OperCode.Rows.Count - 1; i > -1; i--)
            //    {
            //        DataGridViewRow row = DG_OperCode.Rows[i];

            //        for (int j = 0; j < DG_OperCode.ColumnCount; j++)
            //        {
            //            if (!row.IsNewRow && row.Cells[j].Value == null)
            //                DG_OperCode.Rows.RemoveAt(i);
            //        }
            //    }
            //}
        }
    }
}
