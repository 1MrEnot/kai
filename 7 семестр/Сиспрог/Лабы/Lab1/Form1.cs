namespace Sys
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private bool _firstPassError;
        private FirstSecondPass _pass;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _firstPassError = true;
            _pass = new FirstSecondPass();
            Clear_tables();
            FirstSecondPass.DeleteEmptyRows(DG_Source);

            var arrSourceCode = new string[DG_Source.RowCount - 1, DG_Source.Columns.Count];
            for (var i = 0; i < DG_Source.RowCount - 1; i++)
                for (var j = 0; j < DG_Source.Columns.Count; j++)
                    arrSourceCode[i, j] = Convert.ToString(DG_Source.Rows[i].Cells[j].Value);

            var arrOperCode = new string[DG_OperCode.RowCount - 1, DG_OperCode.Columns.Count];
            for (var i = 0; i < DG_OperCode.RowCount - 1; i++)
                for (var j = 0; j < DG_OperCode.Columns.Count; j++)
                {
                    arrOperCode[i, j] = Convert.ToString(DG_OperCode.Rows[i].Cells[j].Value);
                    arrOperCode[i, j] = arrOperCode[i, j].ToUpper();
                }

            //проверяем таблицу кодов операций
            if (_pass.CheckOperationCodeTable(ref arrOperCode))
            {
                //делаем первый проход
                if (_pass.FirstPass(arrSourceCode, arrOperCode, dataGrid_supportTable, dataGrid_symbol_table))
                {
                    _firstPassError = false;
                    button2.Enabled = true;
                    AddErrorTextBox(tbErrorOnePass, _pass.ErrorMessage);
                }
                else
                {
                    AddErrorTextBox(tbErrorOnePass, _pass.ErrorMessage);
                }
            }
            else
            {
                AddErrorTextBox(tbErrorOnePass, _pass.ErrorMessage);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DG_Source.Rows.Add("PROG", "START", "100", "");
            DG_Source.Rows.Add("", "JMP", "L1", "");
            DG_Source.Rows.Add("A1", "RESB", "10", "");
            DG_Source.Rows.Add("A2", "RESW", "20", "");
            DG_Source.Rows.Add("B1", "WORD", "40", "");
            DG_Source.Rows.Add("B2", "BYTE", "X" + '"' + "2F4C008A" + '"', "");
            DG_Source.Rows.Add("B3", "BYTE", "C" + '"' + "Hello!" + '"', "");
            DG_Source.Rows.Add("B4", "BYTE", "12", "");
            DG_Source.Rows.Add("L1", "LOADR1", "B1", "");
            DG_Source.Rows.Add("", "LOADR2", "B4", "");
            DG_Source.Rows.Add("", "ADD", "R1", "R2");
            DG_Source.Rows.Add("", "SAVER1", "B1", "");
            DG_Source.Rows.Add("", "INT", "200", "");
            DG_Source.Rows.Add("", "END", "100", "");

            DG_OperCode.Rows.Add("JMP", "01", "4");
            DG_OperCode.Rows.Add("LOADR1", "02", "4");
            DG_OperCode.Rows.Add("LOADR2", "03", "4");
            DG_OperCode.Rows.Add("ADD", "04", "2");
            DG_OperCode.Rows.Add("SAVER1", "05", "4");
            DG_OperCode.Rows.Add("INT", "06", "2");

            DG_Source.Update();
            DG_OperCode.Update();
        }

        private void Clear_tables()
        {
            dataGrid_supportTable.Rows.Clear();
            dataGrid_symbol_table.Rows.Clear();
            tbErrorOnePass.Clear();
            tbErrorTwoPass.Clear();
            tbBinaryCode.Items.Clear();
        }

        private void AddErrorTextBox(TextBox textBoxFirstErrors, string message)
        {
            textBoxFirstErrors.Text += message + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbBinaryCode.Items.Clear();
            tbErrorTwoPass.Clear();

            if (_firstPassError == false)
            {
                if (_pass.Second_pass(tbBinaryCode))
                {
                    AddErrorTextBox(tbErrorTwoPass, _pass.ErrorMessage);
                }
                else
                {
                    AddErrorTextBox(tbErrorTwoPass, _pass.ErrorMessage);
                }
            }
            else
            {
                MessageBox.Show("Выполните первый проход");
            }
        }

        private void dataGrid_Source_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Clear_tables();
            button2.Enabled = false;
        }
    }
}