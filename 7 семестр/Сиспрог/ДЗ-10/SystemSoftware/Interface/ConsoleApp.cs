using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemSoftware.Common;
using SystemSoftware.MacroProcessor;
using SystemSoftware.Resources;

namespace SystemSoftware.Interface
{
	public class ConsoleProgram : AbstractApp
	{
		/// <summary>
		/// Закончен ли 1 проход.
		/// </summary>
		public bool IsFirstRunEnded { get; set; }

		/// <summary>
		/// Идет ли обработка по шагам.
		/// </summary>
		public bool IsProcessingBySteps { get; set; }

		/// <summary>
		/// Закончена ли обработка исходного кода.
		/// </summary>
		public bool IsProcessingEnded { get; set; }

		/// <summary>
		/// Конструктор. Считывает исходники с файла
		/// </summary>
		public ConsoleProgram(string[] args)
		{
			#region Разбор аргyментов командной строки

			switch (args.Length)
			{
				case 1:
					if (args[0].ToUpper() == "-HELP")
					{
						throw new Exception("");
					}

					throw new CustomException(ConsoleMessages.Error_WrongCommandLineArguments);
				case 2:
					if (args[0].ToUpper() == "-IN")
					{
						InputFile = args[1];
					}
					else if (args[0].ToUpper() == "-OUT")
					{
						OutputFile = args[1];
					}
					else
					{
						throw new CustomException(ConsoleMessages.Error_WrongCommandLineArguments);
					}
					break;
				case 4:
					switch (args[0].ToUpper())
					{
						case "-IN":
						{
							InputFile = args[1];
							if (args[2].ToUpper() == "-OUT")
							{
								OutputFile = args[3];
							}
							else
							{
								throw new CustomException(ConsoleMessages.Error_OutputFileArgument);
							}

							break;
						}
						case "-OUT":
						{
							OutputFile = args[1];
							if (args[2].ToUpper() == "-IN")
							{
								InputFile = args[3];
							}
							else
							{
								throw new CustomException(ConsoleMessages.Error_InputFileArgument);
							}

							break;
						}
						default:
							throw new CustomException(ConsoleMessages.Error_WrongCommandLineArguments);
					}
					break;
				default:
					throw new CustomException(ConsoleMessages.Error_WrongArgumentsCount);
			}

			#endregion

			Refresh();
		}

		/// <summary>
		/// Обновить приложение.
		/// </summary>
		public void Refresh()
		{
			try
			{
				var temp = File.ReadAllLines(InputFile);
				SourceCode = new Processor(temp);
				SourceStrings = new List<string>(temp);

				IsFirstRunEnded = false;
				IsProcessingBySteps = false;
				IsProcessingEnded = false;
				SourceCodeIndex = 0;
			}
			catch (Exception)
			{
				throw new CustomException(ConsoleMessages.Error_InputFileNotFound);
			}
		}


		/// <summary>
		/// Следующий шаг выполнения проги
		/// </summary>
		public void NextStep()
		{
			try
			{
				if (!IsProcessingEnded)
				{
					SourceCode.FirstRunStep(SourceCode.SourceCodeLines[SourceCodeIndex++], MacrosStorage.Root);
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				IsProcessingEnded = true;
			}
			catch (CustomException ex)
			{
				IsProcessingEnded = true;
				Helpers.WriteInConsole($@"{ConsoleMessages.Prefix_ErrorInstring} ""{SourceCode.SourceCodeLines[SourceCodeIndex - 1]}"": {ex.Message}");
			}
			if (SourceCodeIndex == SourceCode.SourceCodeLines.Count)
			{
				Helpers.WriteInConsole(ConsoleMessages.ApplicationRunEnded);
				IsProcessingEnded = true;
			}
			//else if (SourceCodeIndex == 0)
			//{
			//	MemoryManager.CollectGarbage(true);
			//}
		}

		/// <summary>
		/// Первый проход
		/// </summary>
		public void FirstRun()
		{
			if (IsProcessingBySteps == true)
			{
				Helpers.WriteInConsole(ConsoleMessages.StepModeActivated);
				return;
			}
			if (IsFirstRunEnded == true)
			{
				Helpers.WriteInConsole(ConsoleMessages.FirstRunEnded);
				return;
			}
			SourceCodeIndex = 0;
			try
			{
				while (true)
				{
					NextStep();
					if (SourceCodeIndex == SourceCode.SourceCodeLines.Count)
					{
						IsFirstRunEnded = true;
						IsProcessingEnded = true;
						return;
					}
					if (IsProcessingEnded) return;
				}
			}
			catch (CustomException ex)
			{
				IsProcessingEnded = true;
				Helpers.WriteInConsole($"{ConsoleMessages.ErrorPrefix} :{ex.Message}");
			}
		}

		/// <summary>
		/// Возвращает строку со справкой по программе
		/// </summary>
		/// <returns></returns>
		public static string GetUserGuide()
		{
			var n = Environment.NewLine;
			var t = Separator.Tab;
			return
				"\r\n==============={ Справка по ключам и параметрам }===============\r\n" +				
				$"\r\n{ConsoleMessages.Help_AllowedKeys}: [-in] [-out] [-help]{n}\r\n" +
				$"-in{t}{ConsoleMessages.Help_InputFileKey}{n}" +
				$"-out{t}{ConsoleMessages.Help_OutputFileKey}{n}" +
				$"-help{t}{t}{ConsoleMessages.Help_HelpKey}.{n}";
		}

		/// <summary>
		/// Менюшка консольного приложения.
		/// </summary>
		public static string GetProgramGuide()
		{
			var n = Environment.NewLine;
			const string t = " - ";
			return
				$"0{t}{ConsoleMessages.Menu_Exit}{n}" +
				$"1{t}{ConsoleMessages.Menu_NextStep}{n}" +
				$"2{t}{ConsoleMessages.Menu_FirstRun}{n}" +
				$"3{t}{ConsoleMessages.Menu_PrintSourceCode}{n}" +
				$"4{t}{ConsoleMessages.Menu_PrintAssemblerCode}{n}" +
				$"5{t}{ConsoleMessages.Menu_PrintVariablesTable}{n}" +
				$"6{t}{ConsoleMessages.Menu_PrintMacrosTable}{n}" +
				$"7{t}{ConsoleMessages.Menu_SaveAssemblerCodeIntoFile}{n}" +
				$"8{t}{ConsoleMessages.Menu_Refresh}{n}" +
				$"9{t}Распечатать таблицу имен макросов{n}";
		}

		/// <summary>
		/// Распечатывает полностью ассемблерный код в консоль.
		/// </summary>
		public void PrintAssemblerCode()
		{
			foreach (var se in SourceCode.AssemblerCode)
			{
				Console.WriteLine(se.ToString());
			}
		}

		/// <summary>
		/// Распечатать ТМО в консоль.
		/// </summary>
		public void PrintTmo()
		{
			foreach (var e in MacrosStorage.Entities)
			{
				Console.WriteLine($"{Separator.Tab}{e.Name}:");
				for (var i = 0; i < e.Body.Count; i++)
				{
					Console.WriteLine(e.Body[i].ToString());
				}
			}
		}

		/// <summary>
		/// Распечатать таблицу глобальных переменных в консоль.
		/// </summary>
		public void PrintVariablesTable()
		{
			foreach (var e in VariablesStorage.Entities)
			{
				Console.WriteLine($"{e.Name} = {e.Value ?? string.Empty}");
			}
		}

		/// <summary>
		/// Распечатать таблицу имен макросов
		/// </summary>
		public void PrintMacroNameTable()
		{
			foreach (var macro in MacrosStorage.GetMacroNames(SourceCode))
				Console.WriteLine($"{macro.Name}. Начало: {macro.Start}. Конец: {macro.Stop}");
		}
	}
}
