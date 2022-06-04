using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        private Pass pass = null;
        public Form1()
        {
            InitializeComponent();
        }
        internal void SetLinePointer(int pos)
        {
            SourceCodeDataGrid.CurrentCell = SourceCodeDataGrid.Rows[pos].Cells[0];
        }
        internal void UpdateExtDefs(IList<DataStorage.ExtDefLine> TableExtDef)
        {
            dataGridViewExtDefs.DataSource = TableExtDef;
        }
        internal void AddError(string str)
        {
            textBoxFirstErrors.Text += str;
        }
        internal void UpdateTextBoxBinCode(IList<DataStorage.BinCodeLine> BinCodeLines)
        {
            textBoxBinCode.Clear();
            foreach (var item in BinCodeLines)
                textBoxBinCode.AppendText($"{item.Prefix} {item.Address} {item.Length} {item.Command} {item.Operands}\n");
        }
        internal void UpdateTSI(IList<object[]> TableSymbolicNames)
        {
            dataGridViewTSI.Rows.Clear();
            foreach (var i in TableSymbolicNames)
                dataGridViewTSI.Rows.Add(i);
        }
        internal void UpdateTableConfig(IList<DataStorage.ConfigLine> TableConfig)
        {
            dataGridViewTN.DataSource = TableConfig;
        }

        string path;
        readonly List<string[]> backupOperations = new List<string[]>();
        readonly List<string[]> backupSourceCode = new List<string[]>();

        private static void LoadLines(IEnumerable<string[]> lines, DataGridView gridView)
        {
            gridView.Rows.Clear();
            foreach (var line in lines)
            {
                gridView.Rows.Add(line);
            }
        }

        private static List<string[]> GetLines(DataGridView gridView)
        {
            var result = new List<string[]>();
            foreach (DataGridViewRow row in gridView.Rows)
                if (!row.IsNewRow)
                    result.Add(ToArray(row));
            return result;
        }

        private List<SourceCode> GetSourceCode()
        {
            var i = 0;
            try
            {
                var lines = GetLines(SourceCodeDataGrid);
                var list = new List<SourceCode>();
                for (i = 0; i < lines.Count; i++)
                {
                    var x = lines[i];
                    list.Add(new SourceCode(x[0], x[1], x[2], x[3]));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка в строке: " + i + " " + ex.Message);
            }
        }
        private List<OperationCode> GetOperationCode()
        {
            return GetLines(OperationCodeDataGrid).Select(
                x => new OperationCode(x[0],x[1], int.Parse(x[2]))).ToList();
        }
        private void ButtonStep_Click(object sender, EventArgs e)
        {
            try
            {
                if (pass == null)
                {
                    InitPass();
                }
                pass.OneStep();
               
                if (pass.EndFlag)
                {
                    FinishPass();
                }
            }
            catch (Exception ex)
            {
                ClearEnvironment();
                textBoxFirstErrors.Text += "Ошибка: " + ex.Message + "\n";
            }
        }
        private void InitPass()
        {
            DenyInput();
            SaveBackup();
            ClearAll();
            pass = new Pass(GetSourceCode(), GetOperationCode(),this);
        }
        private void FinishPass()
        {
            SourceCodeDataGrid.CurrentCell = SourceCodeDataGrid.Rows[0].Cells[0];
            AllowInput();
            //MessageBox.Show("Проход завершен");
            pass = null;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pass == null)
                {
                    InitPass();
                }
                pass.FirstPass();
                FinishPass();
            }
            catch (Exception ex)
            {
                ClearEnvironment();
                textBoxFirstErrors.Text += "Ошибка: " + ex.Message + "\n";
                pass = null;
                AllowInput();
            }
            
        }
        private void LoadBackup()
        {
            LoadLines(backupSourceCode, SourceCodeDataGrid);
            LoadLines(backupOperations, OperationCodeDataGrid);
        }
        private void SaveBackup()
        {
            backupOperations.Clear();
            foreach (var row in GetLines(OperationCodeDataGrid))
                backupOperations.Add(row);

            backupSourceCode.Clear();
            foreach (var row in GetLines(SourceCodeDataGrid))
                backupSourceCode.Add(row);
        }
        private void DenyInput()
        {
            SourceCodeDataGrid.ReadOnly = true;
            OperationCodeDataGrid.ReadOnly = true;
        }
        private void AllowInput()
        {
            SourceCodeDataGrid.ReadOnly = false;
            OperationCodeDataGrid.ReadOnly = false;
        }
        private void ButtonUpload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OPF = new OpenFileDialog
                {
                    InitialDirectory = Directory.GetCurrentDirectory(),
                    Filter = "Файлы txt|*.txt"
                };
                if (OPF.ShowDialog() != DialogResult.OK)
                {
                    return;
                }


                path = OPF.FileName;
                if (!File.Exists(path))
                {
                    MessageBox.Show($"Файл данных \"{path}\" не найден", "Ошибка загрузки из файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string[] lines = File.ReadAllLines(path);
                bool sourceCode = false; // true - если вводим строки исходного кода
                bool operationCode = false; // true - если вводим строки таблицы кодов операций
                var source = new List<string[]>();
                var operations = new List<string[]>();
                foreach (string line in lines)
                {
                    if (line.Length == 0)
                        continue;
                    if (!sourceCode && line.Equals("*source code*"))
                    {
                        sourceCode = true;
                        operationCode = false;
                        continue;
                    }
                    if (sourceCode && line.Equals("*operations code*"))
                    {
                        sourceCode = false;
                        operationCode = true;
                        continue;
                    }

                    if (sourceCode)
                    {
                        source.Add(DataStorage.FormatedString.Decoded(line));
                    }
                    else if (operationCode)
                    {
                        operations.Add(DataStorage.FormatedString.Decoded(line));
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка в файле данных \"{path}\"", "Ошибка загрузки из файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
                ClearAll();
                LoadLines(source, SourceCodeDataGrid);
                LoadLines(operations, OperationCodeDataGrid);
                pass = null;
            }
            catch (Exception ex)
            {
                textBoxFirstErrors.Text += "Ошибка: " + ex.Message + "\n";
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveBackup();

                var SPF = new SaveFileDialog
                {
                    InitialDirectory = Directory.GetCurrentDirectory(),
                    Filter = "Файлы txt|*.txt"
                };
                if (SPF.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                path = SPF.FileName;
                string text = "*source code*\n";
                text += string.Join("\n", GetLines(SourceCodeDataGrid).Select(x => DataStorage.FormatedString.Coded(x)));
                text += "\n*operations code*\n";
                text += string.Join("\n", GetLines(OperationCodeDataGrid).Select(x => DataStorage.FormatedString.Coded(x)));
                File.Delete(path);
                File.AppendAllText(path, text);
            }
            catch (Exception ex)
            {
                textBoxFirstErrors.Text += "Ошибка: " + ex.Message + "\n";
            }
        }
        private static string ToString(DataGridViewCell cell)
        {
            return cell.Value != null ? cell.Value.ToString().Trim() : "";
        }
        private static string[] ToArray(DataGridViewRow row)
        {
            var arr = new string[row.Cells.Count];
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = ToString(row.Cells[i]);
            }
            return arr;
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                LoadBackup();
                ClearAll();
                AllowInput();
                SourceCodeDataGrid.CurrentCell = SourceCodeDataGrid.Rows[0].Cells[0];
                pass = null;
            }
            catch (Exception ex)
            {
                textBoxFirstErrors.Text +=  "Ошибка: " + ex.Message + "\n";
            }
        }
        private void ClearEnvironment()
        {
            textBoxFirstErrors.Clear();
        }
        private void ClearAll()
        {
            dataGridViewTSI.Rows.Clear();
            dataGridViewTN.Rows.Clear(); ;
            textBoxBinCode.Clear();
            dataGridViewExtDefs.Rows.Clear();
            ClearEnvironment();
        }

        private void LoadDefault(int i)
        {
            ClearAll();
            if (i == 0)
            {
                LoadLines(Default.mixed.Split('\n').Select(x => DataStorage.FormatedString.Decoded(x)), SourceCodeDataGrid);
            }
            else if (i == 1)
            {
                LoadLines(Default.relative.Split('\n').Select(x => DataStorage.FormatedString.Decoded(x)), SourceCodeDataGrid);
            }
            else if (i == 2)
            {
                LoadLines(Default.direct.Split('\n').Select(x => DataStorage.FormatedString.Decoded(x)), SourceCodeDataGrid);
            }
            LoadLines(Default.operations.Split('\n').Select(x => DataStorage.FormatedString.Decoded(x)), OperationCodeDataGrid);
            SaveBackup();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                LoadDefault(0);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                LoadDefault(1);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                LoadDefault(2);
            }
        }
        private HashSet<DataGridViewRow> GetSelectedRows(DataGridView dataGrid)
        {
            var cells = dataGrid.SelectedCells;
            var rows = new HashSet<DataGridViewRow>();
            foreach (var i in Enumerable.Range(0, cells.Count))
            {
                var cell = cells[i];
                rows.Add(cell.OwningRow);
            }
            return rows;
        }

        private void RemoveSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GetSelectedRows(SourceCodeDataGrid))
            {
                SourceCodeDataGrid.Rows.Remove(row);
            }
        }

        private void InsertBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GetSelectedRows(SourceCodeDataGrid))
            {
                SourceCodeDataGrid.Rows.InsertCopy(SourceCodeDataGrid.Rows.Count - 1, row.Index);
            }
        }

        private void InsertAfterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GetSelectedRows(SourceCodeDataGrid))
            {
                SourceCodeDataGrid.Rows.InsertCopy(SourceCodeDataGrid.Rows.Count - 1, row.Index + 1);
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GetSelectedRows(OperationCodeDataGrid))
            {
                OperationCodeDataGrid.Rows.Remove(row);
            }
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GetSelectedRows(OperationCodeDataGrid))
            {
                OperationCodeDataGrid.Rows.InsertCopy(OperationCodeDataGrid.Rows.Count - 1, row.Index);
            }
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GetSelectedRows(OperationCodeDataGrid))
            {
                OperationCodeDataGrid.Rows.InsertCopy(OperationCodeDataGrid.Rows.Count - 1, row.Index + 1);
            }
        }

        private void DubleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in GetSelectedRows(SourceCodeDataGrid))
            {
                var temp = new object[SourceCodeDataGrid.Rows[row.Index].Cells.Count];
                for (var i = 0; i < temp.Length; i++)
                {
                    temp[i] = SourceCodeDataGrid.Rows[row.Index].Cells[i].Value;
                }
                SourceCodeDataGrid.Rows.Insert(row.Index + 1, temp);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
