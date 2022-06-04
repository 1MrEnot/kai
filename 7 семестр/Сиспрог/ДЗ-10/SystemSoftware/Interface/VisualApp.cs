using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SystemSoftware.Common;
using SystemSoftware.MacroProcessor;
using SystemSoftware.Resources;

namespace SystemSoftware.Interface
{
	/// <summary>
	/// Приложение с GUI интерфейсом.
	/// </summary>
	public class VisualApp : AbstractApp
	{
		/// <summary>
		/// Конструктор. Считывает исходники с файла.
		/// </summary>
		public VisualApp(IEnumerable<string> sourceCodeText)
		{
			try
			{
				RefreshApplication(sourceCodeText);
			}
			catch (Exception ex) 
			{
				throw new Exception(ex.Message);
			}
			
		}

		/// <summary>
		/// Обновляет результаты предыдущего прохода
		/// </summary>
		private void RefreshApplication(IEnumerable<string> sourceCodeText)
		{
			MacrosStorage.Refresh();
			VariablesStorage.Refresh();
			SourceCode = new Processor(sourceCodeText);
			
			SourceStrings = new List<string>(sourceCodeText);
		}

		/// <summary>
		/// Шаг выполнения программы 1 просмотра
		/// </summary>
		public void NextFirstStep(TextBox tb)
		{
			try
			{
				SourceCode.FirstRunStep(SourceCode.SourceCodeLines[SourceCodeIndex++], MacrosStorage.Root);
			}
			catch (ArgumentOutOfRangeException)
			{
				SourceCodeIndex = 0;
				RefreshApplication(SourceStrings.ToArray());
				MacrosStorage.Refresh();
				VariablesStorage.Refresh();
			}
			catch (CustomException ex)
			{			
				throw new CustomException($@"{ex.Message}");
			}
			catch (Exception e)
			{				
				throw new CustomException($@"{ProcessorErrorMessages.ErrorInLinePrefix} {SourceCode.SourceCodeLines[SourceCodeIndex - 1]}");
			}
		}

		#region Распечатка

		/// <summary>
		/// Печатает исходники SourceStrings в TextBox
		/// </summary>
		public void PrintSourceCode(RichTextBox tb)
		{
			tb.Clear();
			foreach (var str in SourceStrings)
			{
				tb.AppendText(str + Environment.NewLine);
			}
		}

		/// <summary>
		/// Распечатывает полностью ассемблерный код без макросов в таблицу
		/// </summary>
		public void PrintAssemblerCode(TextBoxBase tb)
		{
			tb.Clear();
			foreach (var se in SourceCode.AssemblerCode)
			{
				tb.AppendText(se.ToString().ToUpper() + Environment.NewLine);
			}
		}

		/// <summary>
		/// Распечатать ТМО в таблицу
		/// </summary>
		public void PrintTmo(DataGridView dgv)
		{
			dgv.Rows.Clear();
			for (var i = 0; i < dgv.Rows.Count; i++)
			{
				dgv.Rows.Remove(dgv.Rows[i]);
			}
			foreach (var e in MacrosStorage.Entities)
			{
				var name = e.Name;
				var index = e.Name.IndexOf("_");
				if (index > -1)
				{
					name = name.Remove(index);
				}
				dgv.Rows.Add(name, e.Body.Count > 0 ? e.Body[0].ToString() : "");
				for (var i = 1; i < e.Body.Count; i++)
				{
					dgv.Rows.Add(null, e.Body[i].ToString());
				}
			}
		}

		/// <summary>
		/// Распечатать таблицу глобальных переменных в GUI таблицу.
		/// </summary>
		public void PrintVariablesTable(DataGridView dgv)
		{
			dgv.Rows.Clear();
			for (var i = 0; i < dgv.Rows.Count; i++)
			{
				dgv.Rows.Remove(dgv.Rows[i]);
			}
			foreach (var e in VariablesStorage.Entities)
			{
				dgv.Rows.Add(e.Name, e.Value ?? "");
			}
		}


		/// <summary>
		/// Распечатать таблицу имен макросов
		/// </summary>
		public void PrintMacroNameTable(DataGridView dgv)
		{
			dgv.Rows.Clear();
			for (var i = 0; i < dgv.Rows.Count; i++)
				dgv.Rows.Remove(dgv.Rows[i]);

			foreach (var macro in MacrosStorage.GetMacroNames(SourceCode))
				dgv.Rows.Add(macro.Name, macro.Start, macro.Stop);
		}

		#endregion
	}
}
