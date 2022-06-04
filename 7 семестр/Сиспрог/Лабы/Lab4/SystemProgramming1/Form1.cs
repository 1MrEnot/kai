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

        
        string[,] source_code;
        string[,] OperationCode;

        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
        }

        FirstSecondPass support;
        int prepare_flag = 0;
        int current_row;
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGrid_OperationCode.Rows.Add("JMP", "01", "4");
            dataGrid_OperationCode.Rows.Add("LOADR1", "02", "4");
            dataGrid_OperationCode.Rows.Add("LOADR2", "03", "4");
            dataGrid_OperationCode.Rows.Add("ADD", "04", "2");
            dataGrid_OperationCode.Rows.Add("SAVER1", "05", "4");
            dataGrid_OperationCode.Rows.Add("INT", "06", "2");
            dataGrid_Source.Update();
            dataGrid_OperationCode.Update();

         
            toolStripExamples.SelectedIndex = 0;

            SelectRow(0);
            
        }

        void Clear_tables()
        {
            dataGrid_supportTable.Rows.Clear();
            dataGrid_symbol_table.Rows.Clear();
   
        }

        void SelectRow(int i)
        {
            if (dataGrid_Source.RowCount > 0 && i+1 <= dataGrid_Source.RowCount)
            {
            dataGrid_Source.Rows[i].Cells[0].Selected = true;
            dataGrid_Source.Rows[i].Cells[1].Selected = true;
            dataGrid_Source.Rows[i].Cells[2].Selected = true;
            dataGrid_Source.Rows[i].Cells[3].Selected = true;
            }
        }
        void UnselectRow(int i)
        {
            if (dataGrid_Source.RowCount > 0 && i + 1 <= dataGrid_Source.RowCount )
            {
                dataGrid_Source.Rows[i].Cells[0].Selected = false;
                dataGrid_Source.Rows[i].Cells[1].Selected = false;
                dataGrid_Source.Rows[i].Cells[2].Selected = false;
                dataGrid_Source.Rows[i].Cells[3].Selected = false;
            }
        }
        void UnselectAll()
        {
            if (dataGrid_Source.RowCount>0)
            for (int i = 0; i < dataGrid_Source.RowCount; i++)
            {
                dataGrid_Source.Rows[i].Cells[0].Selected = false;
                dataGrid_Source.Rows[i].Cells[1].Selected = false;
                dataGrid_Source.Rows[i].Cells[2].Selected = false;
                dataGrid_Source.Rows[i].Cells[3].Selected = false;
            }
        }
        void Copy_tables_in_arrays()
        {
            
            for (int i = 0; i < dataGrid_Source.RowCount - 1; i++)
                for (int j = 0; j < dataGrid_Source.Columns.Count; j++)
                    source_code[i, j] = Convert.ToString(dataGrid_Source.Rows[i].Cells[j].Value);
            //создаем динамический массив куда помещаем нашу таблицу кодов операций
            OperationCode = new string[dataGrid_OperationCode.RowCount - 1, dataGrid_OperationCode.Columns.Count];
            for (int i = 0; i < dataGrid_OperationCode.RowCount - 1; i++)
                for (int j = 0; j < dataGrid_OperationCode.Columns.Count; j++)
                {
                    string temp = Convert.ToString(dataGrid_OperationCode.Rows[i].Cells[j].Value);
                    OperationCode[i, j] = temp.ToUpper();
                }
        }
        void ProtectTableOn()
        {
            dataGrid_Source.AllowUserToAddRows = false;
            dataGrid_Source.AllowUserToDeleteRows = false;
            dataGrid_Source.ReadOnly = true;

            dataGrid_OperationCode.AllowUserToAddRows = false;
            dataGrid_OperationCode.AllowUserToDeleteRows = false;
            dataGrid_OperationCode.ReadOnly = true;
        }
        void ProtectTableOff()
        {
            dataGrid_Source.AllowUserToAddRows = true;
            dataGrid_Source.AllowUserToDeleteRows = true;
            dataGrid_Source.ReadOnly = false;

            dataGrid_OperationCode.AllowUserToAddRows = true;
            dataGrid_OperationCode.AllowUserToDeleteRows = true;
            dataGrid_OperationCode.ReadOnly = false;
        }


        bool prepare()
        {
            support = new FirstSecondPass();
            current_row = 0;
            Clear_tables();
            support.Delete_empty_rows(dataGrid_Source);
            support.Delete_empty_rows(dataGrid_OperationCode);
            //создаем динамический массив куда помещаем нашу таблицу исходного кода
            source_code = new string[dataGrid_Source.RowCount - 1, dataGrid_Source.Columns.Count];
            Copy_tables_in_arrays();

            //проверяем таблицу кодов операций
            if (support.Check_operation_code_table(ref OperationCode))
            {
                UnselectAll();
                SelectRow(0);
                support.operation_code_table = OperationCode;
                support.SourceCodeTable = source_code;

                return true; 
            }
            else
            {
                Add_error(textBox_first_errors, support.ErrorMessage);
                return false; 
            }
   

        }

        void Add_error(TextBox textBox_first_errors, string message)
        {
            textBox_first_errors.Text += message + Environment.NewLine;
        }
        private void dataGrid_Source_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Clear_tables();
         
        }

        void Stop()
        {
            ProtectTableOff();
            support = null;
            prepare_flag = 0;
            current_row = 0;
            UnselectAll();
            SelectRow(0);
            первыйПроходToolStripMenuItem.Enabled = true;
            второйПроходToolStripMenuItem.Enabled = true;
        }


        bool Start(int param)
        {
            if (prepare_flag == 0)
            {
                textBox_first_errors.Clear();
                if (prepare())
                {
                    prepare_flag = 1;
                    ProtectTableOn();
                    textBox_first_errors.Clear();
                }
                else
                {
                    второйПроходToolStripMenuItem.Enabled = false;
                    первыйПроходToolStripMenuItem.Enabled = false;
                    return false;
                }
            }

            switch (param)
            {
                
                    //пошаговое выполнение
                case 2:
                    {
                                              
                        if (current_row + 1 <= dataGrid_Source.RowCount)
                        {

                            SelectRow(current_row + 1);
                            UnselectRow(current_row);

                            if (!support.Pass(dataGrid_symbol_table, dataGrid_supportTable, current_row))
                            {
                                Add_error(textBox_first_errors, support.ErrorMessage);
                                return false;
                            }

                            if (support.EndFlag == 1)
                            {
                                Add_error(textBox_first_errors, support.ErrorMessage);
                                Stop();
                                return true;
                            }

                            textBox_first_errors.Clear();
                            Add_error(textBox_first_errors, support.ErrorMessage);
                            current_row++;


                        }
                        else
                        {
                            if (support.EndFlag == 0)
                            {
                                Add_error(textBox_first_errors, "Ошибка: Не найдена директива END");
                                return false;
                            }
                        
                        }
                        break;
                    }
                case 3:
                    {
                        Stop();
                        break;
                    }
                case 4:
                    {

                        for (int r = current_row; r <= dataGrid_Source.RowCount; r++)
                            if (r + 1 <= dataGrid_Source.RowCount)
                            {

                                SelectRow(r + 1);
                                UnselectRow(r);

                                if (!support.Pass(dataGrid_symbol_table, dataGrid_supportTable, r))
                                {
                                    Add_error(textBox_first_errors, support.ErrorMessage);
                                    return false;
                                }

                                if (support.EndFlag == 1)
                                {
                                    Add_error(textBox_first_errors, support.ErrorMessage);
                                    Stop();
                                    return true;
                                }

                                // r++;


                            }
                            else
                            {
                                if (support.EndFlag == 0)
                                {
                                    Add_error(textBox_first_errors, "Ошибка: Не найдена директива END");
                                    return false;
                                }
                            }

                        break;
                    }
               
        }
            return true;
        }

        //полный проход
        private void первыйПроходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Start(4))
            {
                Stop();
            }

        }

        //пошаговый проход по программе
        private void второйПроходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Start(2))
            {
                Stop();
            }
            
        }


        //остановка прохода по программе
        private void toolStripMenuItemSTOP_Click(object sender, EventArgs e)
        {
            if (!Start(3))
            {
                Stop();
            }
        }

      
        private void toolStripExamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripExamples.SelectedIndex > -1)
            {
               
                Clear_tables();
                Stop();
              

                if (toolStripExamples.SelectedIndex == 0)
                {
                    dataGrid_Source.Rows.Clear();
                    dataGrid_Source.Rows.Add("PROG", "START", "100", "");
                    dataGrid_Source.Rows.Add("", "JMP", "L1", "");
                    dataGrid_Source.Rows.Add("A1", "RESB", "10", "");
                    dataGrid_Source.Rows.Add("A2", "RESW", "20", "");
                    dataGrid_Source.Rows.Add("B1", "WORD", "4096", "");
                    dataGrid_Source.Rows.Add("", "LOADR1", "L1", "");
                    dataGrid_Source.Rows.Add("B2", "BYTE", "X" + '"' + "2F4C008A" + '"', "");
                    dataGrid_Source.Rows.Add("B3", "BYTE", "C" + '"' + "Hello!" + '"', "");
                    dataGrid_Source.Rows.Add("B4", "BYTE", "128", "");
                    dataGrid_Source.Rows.Add("L1", "LOADR1", "B1", "");
                    dataGrid_Source.Rows.Add("", "LOADR2", "B4", "");
                    dataGrid_Source.Rows.Add("", "ADD", "R1", "R2");
                    dataGrid_Source.Rows.Add("", "SAVER1", "L1", "");
                    dataGrid_Source.Rows.Add("", "INT", "200", "");
                    dataGrid_Source.Rows.Add("", "END", "100", "");
                }
                //if (toolStripExamples.SelectedIndex == 1)
                //{
                //    dataGrid_Source.Rows.Clear();
                //    dataGrid_Source.Rows.Add("PROG", "START", "100", "");
                //    dataGrid_Source.Rows.Add("", "JMP", "B2", "");
                //    dataGrid_Source.Rows.Add("", "RESB", "10", "");
                //    dataGrid_Source.Rows.Add("", "RESW", "20", "");
                //    dataGrid_Source.Rows.Add("", "WORD", "4096", "");
                //    dataGrid_Source.Rows.Add("", "LOADR1", "B2", "");
                //    dataGrid_Source.Rows.Add("B2", "BYTE", "X" + '"' + "2F4C008A" + '"', "");
                //    dataGrid_Source.Rows.Add("", "BYTE", "C" + '"' + "Hello!" + '"', "");
                //    dataGrid_Source.Rows.Add("", "BYTE", "128", "");
                //    dataGrid_Source.Rows.Add("", "LOADR1", "B2", "");
                //    dataGrid_Source.Rows.Add("", "LOADR2", "B2", "");
                //    dataGrid_Source.Rows.Add("", "ADD", "R1", "R2");
                //    dataGrid_Source.Rows.Add("", "SAVER1", "B2", "");
                //    dataGrid_Source.Rows.Add("", "INT", "200", "");
                //    dataGrid_Source.Rows.Add("", "END", "100", "");

                //}
                //if (toolStripExamples.SelectedIndex == 2)
                //{
                //    dataGrid_Source.Rows.Clear();
                //    dataGrid_Source.Rows.Add("PROG", "START", "100", "");
                //    dataGrid_Source.Rows.Add("", "JMP", "L1", "");
                //    dataGrid_Source.Rows.Add("A1", "RESB", "10", "");
                //    dataGrid_Source.Rows.Add("A2", "RESW", "20", "");
                //    dataGrid_Source.Rows.Add("B1", "WORD", "4096", "");
                //    dataGrid_Source.Rows.Add("", "LOADR1", "L1", "");
                //    dataGrid_Source.Rows.Add("B2", "BYTE", "X" + '"' + "2F4C008A" + '"', "");
                //    dataGrid_Source.Rows.Add("B3", "BYTE", "C" + '"' + "Hello!" + '"', "");
                //    dataGrid_Source.Rows.Add("B4", "BYTE", "128", "");
                //    dataGrid_Source.Rows.Add("L1", "LOADR1", "B1", "");
                //    dataGrid_Source.Rows.Add("", "LOADR2", "B4", "");
                //    dataGrid_Source.Rows.Add("", "ADD", "R1", "R2");
                //    dataGrid_Source.Rows.Add("", "SAVER1", "L1", "");
                //    dataGrid_Source.Rows.Add("", "INT", "200", "");
                //    dataGrid_Source.Rows.Add("", "END", "100", "");
                //}
                //if (toolStripExamples.SelectedIndex == 3)
                //{
                //    dataGrid_Source.Rows.Clear();
                //    dataGrid_Source.Rows.Add("PROG", "START", "100", "");
                //    dataGrid_Source.Rows.Add("", "JMP", "L1", "");
                //    dataGrid_Source.Rows.Add("A1", "RESB", "10", "");
                //    dataGrid_Source.Rows.Add("A2", "RESW", "20", "");
                //    dataGrid_Source.Rows.Add("B1", "WORD", "4096", "");
                //    dataGrid_Source.Rows.Add("", "LOADR1", "L1", "");
                //    dataGrid_Source.Rows.Add("B2", "BYTE", "X" + '"' + "2F4C008A" + '"', "");
                //    dataGrid_Source.Rows.Add("B3", "BYTE", "C" + '"' + "Hello!" + '"', "");
                //    dataGrid_Source.Rows.Add("B4", "BYTE", "128", "");
                //    dataGrid_Source.Rows.Add("L1", "LOADR1", "B1", "");
                //    dataGrid_Source.Rows.Add("", "LOADR2", "B4", "");
                //    dataGrid_Source.Rows.Add("", "ADD", "R1", "R2");
                //    dataGrid_Source.Rows.Add("", "SAVER1", "L1", "");
                //    dataGrid_Source.Rows.Add("", "INT", "200", "");
                //    dataGrid_Source.Rows.Add("", "END", "100", "");

                //}
            }
        }

        private void dataGrid_Source_KeyUp(object sender, KeyEventArgs e)
        {
            int selectedInd = dataGrid_Source.CurrentRow.Index;

            if (e.KeyCode == Keys.Enter)         //добавление новой строки в DG_Source при нажатии на кнопку Enter
                dataGrid_Source.Rows.Insert(selectedInd, "", "", "", "");
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid_OperationCode_KeyUp(object sender, KeyEventArgs e)
        {
            int selectedInd = dataGrid_OperationCode.CurrentRow.Index;

            if (e.KeyCode == Keys.Enter)         //добавление новой строки в DG_Source при нажатии на кнопку Enter
                dataGrid_OperationCode.Rows.Insert(selectedInd, "", "", "", "");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
