using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemSoftware.MacroProcessor;
using SystemSoftware.Resources;

namespace SystemSoftware.Common
{
	public static class Helpers
	{
		public static readonly string[] Symbols = { "#", "$", "%", "!", "@", "^", "&", "*", "-", "[", "\"", "*", "(", ")", "\\",
			"/", "?", "№", ";", ":", "+", "=", "[", "]", "{", "}", "|", "<", ">", "`", "~", ".", ",", "'", " " };
		public static readonly string[] AssemblerDirectives = { "BYTE", "RESB", "RESW", "WORD" };
		public static readonly string[] MacroGenerationDirectives = { "START", "END", "MACRO", "MEND" };
		public static readonly string[] ConditionDirectives = { Directives.If, Directives.Else, Directives.EndIf, Directives.While, Directives.Endw,Directives.Aif, Directives.Ago };
		public static readonly string[] VariableDirectives = { Directives.Variable, Directives.Set, Directives.Increment };
        public static readonly string[] Keywords = { "ADD", "SUB", "JMP",
	        "SAVER0", "SAVER1", "SAVER2", 
	        "LOADR0", "LOADR1", "LOADR2", 
        };
		public static readonly string[] AllKeywords = MacroGenerationDirectives.Concat(ConditionDirectives).Concat(VariableDirectives).Concat(Keywords).ToArray();
		public static readonly string RussianLetters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
		public static readonly string[] ComparisonSigns = { "==", ">=", "<=", "!=", ">", "<" };
		public static readonly string CurrentDirectory = new DirectoryInfo(Environment.CurrentDirectory).FullName;

		/// <summary>
		/// Проверка на директивы препроцессора (ассемблера).
		/// </summary>
		/// <param name="operation">Строка для проверки.</param>
		/// <returns>Является ли операция директивой ассемблера.</returns>
		public static bool IsAssemblerDirective(string operation)
		{
			return operation != null && operation.In(AssemblerDirectives);
		}

		/// <summary>
		/// Проверка на директиву макрогенерации.
		/// </summary>
		/// <param name="operation">Строка для проверки.</param>
		/// <returns>Является ли операция директивой макрогенерации.</returns>
		public static bool IsMacrogenerationDirective(string operation)
		{
			return operation != null && operation.In(MacroGenerationDirectives);
		}

		/// <summary>
		/// Проверка на условные директивы.
		/// </summary>
		/// <param name="operation">Операция для проверки.</param>
		/// <returns>Является ли операция условной директивой.</returns>
		public static bool IsConditionDirective(string operation)
		{
			return operation != null && operation.In(ConditionDirectives);
		}

		/// <summary>
		/// Проверка на директиву для переменных.
		/// </summary>
		/// <param name="operation">Операция для проверки.</param>
		/// <returns>Является ли операция директивой для переменных.</returns>
		public static bool IsVariableDirective(string operation)
		{
			return operation != null && operation.ToUpper().In(VariableDirectives.Select(x => x.ToUpper()).ToArray());
		}

		/// <summary>
		/// Проверка на метку.
		/// </summary>
		public static bool IsLabel(string label, bool isCheckParent = false, string parent = "")
		{
			if (!IsOperation(label))
			{
				return false;
			}
			if (AllKeywords.Contains(label))
			{
				return false;
			}
			if (!IsNotRussian(label))
			{
				return false;
			}
			if (!isCheckParent)
			{
                if (label.Contains("_"))
                {
					if (MacrosStorage.IsInTMO(label))
					{
						return false;
					}
				}
                else
                {
					return MacrosStorage.Entities.FirstOrDefault(m => m.Name == label) == null;
                }				
			}
			else
			{
				if (MacrosStorage.IsInTMO(label, parent))
				{
					return false;
				}
			}
			if (VariablesStorage.IsInVariablesStorage(label))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Проверка на операцию.
		/// </summary>
		public static bool IsOperation(string operation)
		{
			if (string.IsNullOrEmpty(operation)) return false;

			// Should not begin with digit
			if (char.IsDigit(operation[0])) return false;

			// Should not contain incorrect symbols like #, $, ...
			if (operation.Any(x => x.ToString().In(Symbols))) return false;

			// Should not be a register
			if (IsRegister(operation)) return false;

			// Should not be a directive
			if (IsAssemblerDirective(operation)) return false;

			return true;
		}

		/// <summary> 
		/// Проверка на ключевое слово.
		/// </summary>
		/// <param name="operation">Операция для проверки.</param>
		/// <returns>Является ли операция ключевым словом.</returns>
		public static bool IsKeyWord(string operation)
		{
			return operation != null && operation.In(Keywords);
		}

		/// <summary> 
		/// Проверка на регистр.
		/// </summary>
		/// <param name="operation">Операция для проверки.</param>
		/// <returns>Является ли операция регистром.</returns>
		public static bool IsRegister(string operation)
		{
			for (var i = 0; i < 16; i++)
			{
				if ("R" + i == operation.Trim().ToUpper())
					return true;
			}
			return false;
		}

		/// <summary>
		/// Проверка на присутствие русских символов.
		/// </summary>
		public static bool IsNotRussian(string word)
		{
			return !word.Any(x => x.ToString().In(RussianLetters));
		}

		#region Comparison coditions for IF-AIF-WHILE

		/// <summary> \
		/// Get condition parts from string like "a > b"
		/// </summary>
		public static void ParseCondition(string str, out string first, out string second, out string sign, out bool isNums, Macro current)
		{

			int firstNum;
			int secondNum;			
			string[] arr;
			first = "";
			second = "";
			sign = "";
			int temp;
			foreach (var sgn in ComparisonSigns)
			{
				if ((arr = str.Split(new string[] { sgn }, StringSplitOptions.None)).Length > 1)
				{
					if (VariablesStorage.IsInVariablesStorage(arr[0]))
					{
						if (VariablesStorage.Find(arr[0]).Value == null)
						{
							throw new CustomException($"{ProcessorErrorMessages.EmptyVariableInComparison} ({arr[0]})");
						}

						first = VariablesStorage.Find(arr[0]).Value;

					}
                    else if (current.Parameters.FirstOrDefault(p=>p.Name == arr[0]) != null)
                    {
						var param = current.Parameters.FirstOrDefault(p => p.Name == arr[0]);
						if (param.Value == null) throw new CustomException("Попытка получение параметра без значения");
						first = param.Value;
					}
                    //               else if (int.TryParse(arr[0], out temp) == false)
                    //{
                    //	throw new CustomException($"{ProcessorErrorMessages.UndefinedComparisonPart} ({arr[0]})");
                    //}
                    else
                    {
                        first = arr[0];
                    }

					if(str.Split(new string[]{ sgn},StringSplitOptions.None).Length > 2)
                    {
						arr[1] = str.Substring(str.IndexOf(sgn) + sgn.Length);
                    }

                    if (VariablesStorage.IsInVariablesStorage(arr[1]))
                    {
	                    if (VariablesStorage.Find(arr[1]).Value == null)
						{
							throw new CustomException($"{ProcessorErrorMessages.EmptyVariableInComparison} ({arr[1]})");
						}

	                    second = VariablesStorage.Find(arr[1]).Value;
                    }
					else if (current.Parameters.FirstOrDefault(p => p.Name == arr[1]) != null)
					{
						var param = current.Parameters.FirstOrDefault(p => p.Name == arr[1]);
						if (param.Value == null) throw new CustomException("Попытка получение параметра без значения");
						second = param.Value;
					}
                    //else if (int.TryParse(arr[1], out temp) == false)
                    //{
                    //	throw new CustomException($"{ProcessorErrorMessages.UndefinedComparisonPart} ({arr[1]})");
                    //}
                    else
                    {
                        second = arr[1];
                    }

                    sign = sgn;
					isNums = Int32.TryParse(first, out firstNum) && Int32.TryParse(second, out secondNum);
					return;
				}
			}			
			throw new CustomException(ProcessorErrorMessages.UndefinedComparisonSign);
		}

		public static void PushConditionArgs(string str, Macro te)
		{
			string first, second;
			string sign;
			bool isNums;			
			Helpers.ParseCondition(str, out first, out second, out sign, out isNums, te);
			string[] arr;
			var list = new List<string>();
			if ((arr = str.Split(new string[] { sign }, StringSplitOptions.None)).Length == 2)
			{
				if (VariablesStorage.IsInVariablesStorage(arr[0]))
				{
					list.Add(arr[0]);
				}
				if (VariablesStorage.IsInVariablesStorage(arr[1]))
				{
					list.Add(arr[1]);
				}
			}
			var dict = new Dictionary<List<string>, Macro>();
			dict.Add(list, te);
			VariablesStorage.WhileVar.Push(dict);
		}


		/// <summary> Сравнение
		/// </summary>
		public static bool Compare(string str, Macro current)
		{
			string first, second;			
			string sign;
			bool isNums;
			ParseCondition(str, out first, out second, out sign, out isNums, current);
            if (isNums)
            {
				switch (sign)
				{
					case ">":
						return int.Parse(first) > int.Parse(second);
					case "<":
						return int.Parse(first) < int.Parse(second);
					case ">=":
						return int.Parse(first) >= int.Parse(second);
					case "<=":
						return int.Parse(first) <= int.Parse(second);
					case "==":
						return int.Parse(first) == int.Parse(second);
					case "!=":
						return int.Parse(first) != int.Parse(second);
					default:
						return false;
				}
			}

            switch (sign)
            {
	            case ">":
		            throw new CustomException("Невозможно применить данное сравнение к операндам");
	            case "<":
		            throw new CustomException("Невозможно применить данное сравнение к операндам");
	            case ">=":
		            throw new CustomException("Невозможно применить данное сравнение к операндам");
	            case "<=":
		            throw new CustomException("Невозможно применить данное сравнение к операндам");
	            case "==":
		            return first == second;
	            case "!=":
		            return first != second;
	            default:
		            return false;
            }

		}

		#endregion

		/// <summary>
		/// Проверка на совпадение имен.
		/// </summary>
		/// <param name="name">Имя для проверки.</param>
		public static void CheckNames(string name, bool isForeChild = false, bool isParameter = false)
		{
			var list = new List<string>();
			foreach (var glob in VariablesStorage.Entities)
			{
				list.Add(glob.Name);
			}
			if (!isForeChild)
			{
				foreach (var te in MacrosStorage.Entities)
				{
					list.Add(te.Name);
				}
			}
			else
			{
				foreach (var te in MacrosStorage.Entities.Where(e => e.ParentMacros == null || e.ParentMacros.Name == null))
				{
					list.Add(te.Name);
				}
			}
			if (!isParameter)
			{
				foreach (var p in MacrosStorage.Entities.SelectMany(e => e.Parameters))
				{
					list.Add(p.Name);
				}
			}

			if (list.Contains(name))
			{
				throw new CustomException($"{ProcessorErrorMessages.NameIsAleradyUsed} ({name})");
			}
		}

		public static CodeEntity Print(CodeEntity str)
		{
			var newStr = str.Clone() as CodeEntity;
			for (var j = 0; j < newStr.Operands.Count; j++)
			{
				if (VariablesStorage.IsInVariablesStorage(newStr.Operands[j]))
				{
					//if (VariablesStorage.Find(newStr.Operands[j]).Value.HasValue)
						newStr.Operands[j] = VariablesStorage.Find(newStr.Operands[j]).Value;					
					//else
						//throw new CustomException($"{ProcessorErrorMessages.NullVariable} ({newStr.Operands[j]})");
				}
				else
                {
					if(newStr.MacroParentName != null)
                    {
						var currentMacro = MacrosStorage.Entities.FirstOrDefault(m => m.Name == newStr.MacroParentName);
						if (currentMacro != null && currentMacro.Parameters.FirstOrDefault(p => p.Name == newStr.Operands[j]) != null)
						{
							newStr.Operands[j] = currentMacro.Parameters.FirstOrDefault(p => p.Name == newStr.Operands[j])?.Value?.ToString();
						}
					}					
				}
			}
			return newStr;
		}

		/// <summary>
		/// Записать сообщение в консоль с разделителями в виде новой строки.
		/// </summary>
		/// <param name="message">Строка, которою надо записать в консоль.</param>
		public static void WriteInConsole(string message)
		{
			Console.WriteLine($"{Environment.NewLine}{message}{Environment.NewLine}");
		}
	}

	public class CustomException : Exception
	{
		public CustomException(string message)
			: base(message)
		{
			var a = 1;
		}
	}
}
