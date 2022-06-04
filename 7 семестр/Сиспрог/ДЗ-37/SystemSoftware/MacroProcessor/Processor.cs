using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SystemSoftware.Common;
using SystemSoftware.Resources;

namespace SystemSoftware.MacroProcessor
{
	/// <summary>
	/// Процессор. Обработка макрогенерации.
	/// </summary>
	public class Processor
	{
		public int lineCount;
		public int currentNumLine;
		/// <summary>
		/// Вложенность макроопределений.
		/// </summary>
		private int _macroCount { get; set; }

		/// <summary>
		/// Название текущего макроопределения.
		/// </summary>
		public string _currentMacroName { get; set; }

		/// <summary>
		/// Список строк, подозрительных на макровызов.
		/// </summary>
		private List<CodeEntity> PseudoMacroCalls { get; set; } = new List<CodeEntity>();

		/// <summary>
		/// Список строк исходного кода.
		/// </summary>
		public List<CodeEntity> SourceCodeLines { get; set; }

		/// <summary>
		/// Результаты первого прохода - ассемблерный код.
		/// </summary>
		public List<CodeEntity> AssemblerCode { get; set; } = new List<CodeEntity>();

		/// <summary>
		/// Парсер параметров.
		/// </summary>
		public readonly MacroParametersParser MacroParametersParser;
		private int macroNumber;

		

		public Processor(IEnumerable<string> strs)
		{
			currentNumLine = 0;
			lineCount = strs.Where(s=>s!="\r" && s!="").Count();
			//парсим строки в объектное представление

			SourceCodeLines = ParseSourceCode(strs);
			
			//назначаем родителя для исходных строк
			foreach (var se in SourceCodeLines)
			{
				se.Sources = this;
			}
			PseudoMacroTableSingletone.Clear();
			MacroParametersParser = new MixedMacroParametersParser(this);
		}

		public Processor(List<CodeEntity> strs)
		{
			//парсим строки в объектное представление
			SourceCodeLines = strs;
			//назначаем родителя для исходных строк
			foreach (var se in SourceCodeLines)
			{
				se.Sources = this;
			}
			MacroParametersParser = new MixedMacroParametersParser(this);
		}

		public void MacroSubstitution(CodeEntity se, Macro te) //подстановка
		{
			var localMbMacroCall = new List<CodeEntity>();
			foreach (var mc in PseudoMacroCalls)
			{
				var fixedMacroName = _currentMacroName;
				if(_currentMacroName!= null && _currentMacroName.Contains("_"))
                {
					var index = _currentMacroName.IndexOf("_");
					if (index > -1)
					{
						fixedMacroName = _currentMacroName.Remove(index);
					}
				}
				if (mc.Operation == fixedMacroName)
				{
					var currentTe = MacrosStorage.SearchInTMO(_currentMacroName);
					MacrosStorage.CurrentMacro = currentTe;
					currentTe.PreviousMacros = te;
					CheckSourceEntity.CheckMacroSubstitution(mc, currentTe);
					CheckBody.CheckMacroRun(mc, te, currentTe);

					// Обработаем параметры макроса
					var processedMacroBody = ProcessMacroParams(currentTe, mc.Operands);

					var calledProcessor = currentTe.CallMacro(processedMacroBody);
					localMbMacroCall.AddRange(calledProcessor.PseudoMacroCalls);

					// результат макроподстановки
					var macroSubs = new List<CodeEntity>();
					foreach (var str in calledProcessor.AssemblerCode)
					{
						var macroSubsEntity = Helpers.Print(str);
						macroSubs.Add(macroSubsEntity);
						if (!Helpers.IsAssemblerDirective(macroSubsEntity.Operation) && !Helpers.IsKeyWord(macroSubsEntity.Operation))
						{
							localMbMacroCall.Add(macroSubsEntity);
							macroSubsEntity.IsRemove = true;
							var list = PseudoMacroCalls.FindAll(x => x.Operation == macroSubsEntity.Operation);
							if (list.Count > 0)
							{
								var n = list.Max(x => x.CallNumber);
								macroSubsEntity.CallNumber = n++;
							}
						}
					}
					// Заменяем в результате макровызов на результат макроподстановки
					for (var i = 0; i < AssemblerCode.Count; i++)
					{
						if (AssemblerCode[i].Operation == mc.Operation && AssemblerCode[i].IsRemove == true && mc.CallNumber == AssemblerCode[i].CallNumber)
						{
							AssemblerCode.Remove(AssemblerCode[i]);
							AssemblerCode.InsertRange(i, macroSubs);
							i += macroSubs.Count - 1;
						}
					}
				}
			}
			foreach (var str in localMbMacroCall)
			{
				PseudoMacroCalls.Add(str);
				PseudoMacroTableSingletone.AddSourceEntity(se);
			}

			foreach (var sourceEntity in PseudoMacroCalls)
			{				
				//Macro currentTe = MacrosStorage.FindVisibleMacro(te, sourceEntity.Operation);
				var currentTe = MacrosStorage.FindVisibleMacro(MacrosStorage.Entities.FirstOrDefault(m=>m.Name == se.Sources._currentMacroName), sourceEntity.Operation);
				if (MacrosStorage.IsInTMO(sourceEntity.Operation))
				{
					//CheckBody.CheckMacroRun(sourceEntity, te, currentTe);
					CheckBody.CheckMacroRun(sourceEntity, te, currentTe);
				}
			}
		}

		/// <summary>
		/// Шаг первого прохода
		/// </summary>
		public void FirstRunStep(CodeEntity se, Macro te)
		{
			currentNumLine++;
			var operation = se.Operation;
			var label = se.Label;
			var operands = se.Operands;

			CheckSourceEntity.CheckLabel(se);
			if (operation == "END")
			{
				CheckSourceEntity.CheckEnd(se, _macroCount);
				PseudoMacroTableSingletone.CheckPseudoMacro();
				AssemblerCode.Add(Helpers.Print(se));
			}
			else if (operation == Directives.Variable && _macroCount == 0)
			{
				CheckSourceEntity.CheckVariable(se);
				if (operands.Count == 1)
					VariablesStorage.Entities.Add(new Variable(operands[0], null));
				else
					VariablesStorage.Entities.Add(new Variable(operands[0], operands[1]));
			}
			else if (operation == "SET" && _macroCount == 0)
			{
				var foundInVar = false;
				CheckSourceEntity.CheckSet(se, te, out foundInVar);
				if(foundInVar)
					VariablesStorage.Find(se.Operands[0]).Value = se.Operands[1];
			}
			else if (operation == "INC" && _macroCount == 0)
			{
				var foundInVar = false;
				CheckSourceEntity.CheckInc(se, te, out foundInVar);
                if (foundInVar)
                {
                    try
                    {
						var variable = VariablesStorage.Find(operands[0]);
						var number = Int32.Parse(variable.Value);
						variable.Value = (number+1).ToString();
					}
                    catch
                    {
						throw new CustomException("Значение переменной нельзя инкрементировать");
                    }									
				}
					
			}
			else if (operation == "MACRO")
			{
				var macroNameRegex = new Regex("^([a-zA-Z][a-zA-Z0-9]*)$");
				var macroName = se.ToString().Split(':')[0];
				if (!macroNameRegex.IsMatch(macroName)) throw new CustomException($"Некорректное имя макроса {macroName}. Содержит недопустимые символы и/или имеет некорректный формат.");
				//if (operands.Count != 0)
				//{
				//    throw new CustomException("Параметры в макросах запрещены");
				//}

				if (_macroCount == 0)
				{
					macroNumber++;
					CheckSourceEntity.CheckMacro(se, _macroCount);
					var currentTe = new Macro()
					{
						Name = label,
						Parameters = MacroParametersParser.Parse(operands, label)
					};
					currentTe.ParentMacros = te;
					te.ChildrenMacros.Add(currentTe);
					MacrosStorage.Entities.Add(currentTe);
					CheckMacros.CheckLocalTmo();
					_currentMacroName = label;

					if (Config.FORBID_CALL_BEFORE_DEFENITION)
					{
						// Ругаемся на не опережающее определение макроса
						if (AssemblerCode.Any(code => code.Operation == label))
							throw new CustomException($"Макрос {label} был определён после вызова");
					}
				}
				else if (_macroCount > 0)
				{
					//var currentMacro = MacrosStorage.Entities.FirstOrDefault(m => m.Name == _currentMacroName);
					//var parent = currentMacro.ParentMacros;
					//while (true)
					//{
					//	if (currentMacro.ParentMacros == null || currentMacro.ParentMacros.Name == null)
					//	{
					//		if (MacrosStorage.SearchAsNotChildMacro(se.Label) != null || MacrosStorage.SearchAsChildMacro(se.Label, currentMacro.Name) != null)
					//		{
					//			throw new CustomException("Макрос с данным именем уже объявлен в родительском макросе");
					//		}
     //                       else
     //                       {
					//			break;
     //                       }
					//	}
					//	else
					//	{
					//		if (MacrosStorage.SearchAsChildMacro(se.Label, currentMacro.ParentMacros.Name) != null)
					//		{
					//			throw new CustomException("Макрос с данным именем уже объявлен в родиетльском макросе");
					//		}
					//	}
					//	currentMacro = parent.ParentMacros;
					//}
                   

					se.Label = se.Label + "_" + macroNumber;
					se.IsMacroChild = true;
					se.MacroParentName = _currentMacroName;
					MacrosStorage.SearchInTMO(_currentMacroName).Body.Add(se);
				}
				else
				{
					throw new CustomException(ProcessorErrorMessages.IncorrectMacroMendCount);
				}
				_macroCount++;
			}
			else if (operation == "MEND")
			{
				if (_macroCount > 1)
				{
					MacrosStorage.SearchInTMO(_currentMacroName).Body.Add(se);
				}
				CheckSourceEntity.CheckMend(se, _macroCount);
				_macroCount--;
				// MacrosStorage.CurrentMacro = MacrosStorage.CurrentMacro?.ParentMacros;
				if (_macroCount == 0)
				{
					MacroSubstitution(se, te);
				}
			}
			else
			{
				if (_macroCount > 0)
				{
					if (_currentMacroName.Contains("_"))
					{
						MacrosStorage.SearchInTMO(_currentMacroName).Body.Add(se);
					}
					else
					{
						MacrosStorage.Entities.FirstOrDefault(m => m.Name == _currentMacroName).Body.Add(se);
					}
					//MacrosStorage.SearchInTMO(_currentMacroName).Body.Add(se);
				}
				else
				{
					if (te == MacrosStorage.Root && operation.In("IF", "ELSE", "ENDIF", "WHILE", "ENDW" ,"AIF", "AGO"))
					{
						throw new CustomException($"{ProcessorErrorMessages.UseOnlyInsideMacro_1} {operation} {ProcessorErrorMessages.UseOnlyInsideMacro_2} ({se.SourceString})");
					}
					// макровызов
					//if (MacrosStorage.IsInTMO(operation, te?.GetFunkingName()==operation?null: te?.Name))
					var currentMacro = _currentMacroName == null? null:MacrosStorage.SearchInTMO(_currentMacroName);
					var name = te?.Name;
					if(MacrosStorage.FindVisibleMacro(te, operation) !=null)
					//if (MacrosStorage.IsInTMOAsChildOrNot(operation, te?.Name))					
					{
						//Macro currentTe = MacrosStorage.SearchInTMO(operation, te?.GetFunkingName() == operation ? null : te?.Name);
						//Macro currentTe = MacrosStorage.SearchInTMOAsChildOrNot(operation, te?.Name);
						var currentTe = MacrosStorage.FindVisibleMacro(te, operation);
						MacrosStorage.CurrentMacro = currentTe;
						currentTe.PreviousMacros = te;
						CheckSourceEntity.CheckMacroSubstitution(se, currentTe);
						CheckBody.CheckMacroRun(se, te, currentTe);

						// Обработаем параметры макроса
						var processedMacroBody = ProcessMacroParams(currentTe, operands);

						var calledProcessor = currentTe.CallMacro(processedMacroBody);

						// Если после макровызова есть описанный, но не вызванный макрос - ошибка
						foreach (var sourceEntity in PseudoMacroCalls)
						{
							var teCur = MacrosStorage.SearchInTMO(sourceEntity.Operation);
							if (MacrosStorage.IsInTMO(sourceEntity.Operation))
							{
								CheckBody.CheckMacroRun(sourceEntity, te, teCur);
							}
						}

						PseudoMacroCalls.AddRange(calledProcessor.PseudoMacroCalls);
						foreach (var str in calledProcessor.AssemblerCode)
						{
							AssemblerCode.Add(Helpers.Print(str));
						}
					}
					else
					{
						// Добавляем строку в список подозрительных на макровызов и в результат
						se.MacroParentName = te.Name;
						se = Helpers.Print(se);
						if (_macroCount == 0 && !Helpers.IsAssemblerDirective(se.Operation) && !Helpers.IsKeyWord(se.Operation))
						{
							se.IsRemove = true;
							var list = PseudoMacroCalls.FindAll(x => x.Operation == se.Operation);
							if (list.Count > 0)
							{
								var n = list.Max(x => x.CallNumber);
								se.CallNumber = n + 1;
							}
							PseudoMacroCalls.Add(se);
							PseudoMacroTableSingletone.AddSourceEntity(se);
						}
						if (te != MacrosStorage.Root && se.Label != null)
						{
							//var newLabel = $"{se.Label}_{te.Name}_{te.UniqueLableForLoop}";
							var newLabel = $"$_{se.Label}_{te.Name}_{te.UniqueLableForLoop}";
							se.Label = newLabel;							
							te.UniqueLableForLoop++;
						}
						AssemblerCode.Add(se);
					}
				}
			}

            if (/*currentNumLine == lineCount-1 ||*/ currentNumLine == lineCount)
            {
                //CheckSourceEntity.CheckEnd(se, _macroCount);
                PseudoMacroTableSingletone.CheckPseudoMacro();
				CheckSourceEntity.CheckEnd(se, _macroCount);
				//AssemblerCode.Add(Helpers.Print(se));
			}
        }
		
		/// <summary>
		/// Парсит массив строк в масссив SourceEntity, но только до появления первого END в качестве операции
		/// </summary>
		public static List<CodeEntity> ParseSourceCode(IEnumerable<string> strs)
		{
			var result = new List<CodeEntity>();
			foreach (var s in strs)
			{
				// пропускаем пустую строку
				if (String.IsNullOrEmpty(s.Trim()))
					continue;
				var currentString = s.ToUpper().Trim();
				var se = new CodeEntity() { SourceString = currentString };

				//разборка метки
				if (currentString.Contains(':') && (!currentString.Contains("BYTE") ||  currentString.IndexOf(':') < currentString.IndexOf("C'")))
				{
					se.Label = currentString.Split(':')[0].Trim();
					currentString = currentString.Remove(0, currentString.Split(':')[0].Length + 1).Trim();
				}

				if (currentString.Split(null as char[], StringSplitOptions.RemoveEmptyEntries).Length > 0)
				{
					se.Operation = currentString.Split(null as char[], StringSplitOptions.RemoveEmptyEntries)[0].Trim();
					currentString = currentString.Remove(0, currentString.Split(null as char[], StringSplitOptions.RemoveEmptyEntries)[0].Length).Trim();
				}

				if (se.Operation == "BYTE")
				{
					se.Operands.Add(currentString.Trim());
				}
				else
				{
					for (var i = 0; i < currentString.Split(null as char[], StringSplitOptions.RemoveEmptyEntries).Length; i++)
					{
						se.Operands.Add(currentString.Split(null as char[], StringSplitOptions.RemoveEmptyEntries)[i].Trim());
					}
				}

				//название проги или макроса - в поле метки
				if (se.Operands.Count > 0 && se.Operands[0] == "MACRO")
				{
					se.Label = se.Operation;
					se.Operation = se.Operands[0];
					for (var i = 1; i < se.Operands.Count; i++)
					{
						se.Operands[i - 1] = se.Operands[i];
					}
					se.Operands.RemoveAt(se.Operands.Count - 1);
				}
				result.Add(se);
				
				if (se.Operation == "END")
				{
					return result;
				}
			}
			
			return result;
		}

		/// <summary>
		/// Произвести подстановку параметров при макровызове.
		/// </summary>
		/// <param name="macro">Макрос.</param>
		/// <param name="passedParams">Переданные параметры.</param>
		/// <returns>Результат подстановки параметров - измененное тело макроса.</returns>
		private List<CodeEntity> ProcessMacroParams(Macro macro, IEnumerable<string> passedParams)
		{
			// First key param index in the params list
			var firstKeyParamIndex = -1;

			#region Check correctness of passed params

			var keyCount = macro.Parameters.Where(p => p.Type == MacroParameterTypes.Key).Count();

			if (!(macro.Parameters.Count - keyCount<= passedParams.Count() && passedParams.Count()<=macro.Parameters.Count()))
			{
				throw new CustomException(string.Format(
					ProcessorErrorMessages.IncorrectMacroCallParametersCount, macro.Name, passedParams.Count(), $"{macro.NonKeyParametersCount} ({macro.Parameters.Count})" ));
			}

			var first = passedParams.FirstOrDefault(x => new Regex("^(.+=.*)$").IsMatch(x));
			if (first != null)
			{
				firstKeyParamIndex = Array.IndexOf(passedParams.ToArray(), first);				
				if (passedParams.Any(x => !x.Contains("=") && Array.IndexOf(passedParams.ToArray(), x) > firstKeyParamIndex))
				{
					throw new CustomException(string.Format(ProcessorErrorMessages.IncorrectMacroCallParameters, macro.Name));
				}

				//if (MacrosStorage.CurrentMacro.NonKeyParametersCount < firstKeyParamIndex)
    //            {
				//	throw new CustomException("Передано неверное количество неключевых параметров");
    //            }
			}

            //if (firstKeyParamIndex != macro.FirstKeyParameterIdx && first==null)
            //{
            //    throw new CustomException(string.Format(ProcessorErrorMessages.IncorrectMacroCallParameters, macro.Name));
            //}

            #endregion

            // формируем локальную область видимости (параметры в виде key-value)
            var dict = new Dictionary<string, string>();

			// Delegate to process positioned params
			Action ParsePositionedParams = delegate ()
			{
				// If there is no key params, get all params as positioned
				var localFirstParamIndex = firstKeyParamIndex >= 0 ? firstKeyParamIndex : passedParams.Count();
				if(localFirstParamIndex != MacrosStorage.CurrentMacro.NonKeyParametersCount)
                {
					throw new CustomException("Передано неверное количество неключевых параметров");
				}
				for (var i = 0; i < localFirstParamIndex; i++)
				{
					var currentParam = passedParams.ToArray()[i];
					var variable = VariablesStorage.Entities.FirstOrDefault(e => e.Name.EqualsIgnoreCase(currentParam));
					//if (variable == null && !int.TryParse(currentParam, out int temp))
					//{
					//	throw new CustomException(string.Format(ProcessorErrorMessages.IncorrectMacroParameterValue, currentParam));
					//}
					if (variable != null && variable.Value == null)
					{
						throw new CustomException(string.Format(ProcessorErrorMessages.MacroParameterIsEmptyVariable, currentParam));
					}

					MacroParameter param = null;
					if(variable == null && !MacrosStorage.CurrentMacro.PreviousMacros.IsRootMacro)
                    {
						param = MacrosStorage.CurrentMacro.PreviousMacros.Parameters.FirstOrDefault(p => p.Name.EqualsIgnoreCase(currentParam));
					}

					var curerntParamValue = variable?.Value ?? param?.Value ?? currentParam;
					dict.Add(macro.Parameters[i].Name, curerntParamValue);
					macro.Parameters[i].Value = curerntParamValue;
				}
			};

			// Delegate to process key params
			Action ParseKeyParams = delegate ()
			{
				// If there is no key params - exit
				if (firstKeyParamIndex < 0) 
				{
					var keyParams = macro.Parameters.Where(p => p.Type == MacroParameterTypes.Key);
					foreach (var k in keyParams) 
					{
						if(String.IsNullOrWhiteSpace(k.DefaultValue))
							throw new CustomException("Не заданы значения для всех обязательных ключевых параметров");
						//var value = k.DefaultValue ??
						//throw new CustomException(string.Format(ProcessorErrorMessages.IncorrectMacroParameterValue, k.Name));
						dict.Add(k.Name, k.DefaultValue);
					}
					return;
				}

				for (var i = firstKeyParamIndex; i < passedParams.Count(); i++)
				{
					var currentParameter = passedParams.ToArray()[i];
					var vals = currentParameter.Split('=');
					if (vals.Length < 2)
					{
						throw new CustomException(string.Format(ProcessorErrorMessages.IcorrectMacroCallKeyParameter, macro.Name, currentParameter));
					}

					//if(String.IsNullOrWhiteSpace(vals[1]))
     //               {
					//	throw new CustomException("Задание пустого значения ключевого параметра");
     //               }

					var macroParameter = macro.Parameters.FirstOrDefault(e => e.Name == vals[0]);
					if (macroParameter == null)
					{
						throw new CustomException(string.Format(ProcessorErrorMessages.ParameterDoesNotExists, vals[0]));
					}
					if (macroParameter.Type != MacroParameterTypes.Key)
					{
						throw new CustomException(string.Format(ProcessorErrorMessages.IncorrectMacroParameterType, vals[0]));
					}

					var passedValue = vals.Length==2? vals[1]: currentParameter.Substring(currentParameter.IndexOf('=')+1);				
					string value = null;
					if (string.IsNullOrEmpty(passedValue))
					{
						throw new CustomException("Задание пустого значения ключевого параметра");
						// значение не указали
						//value = macroParameter.DefaultValue ??
						//throw new CustomException(string.Format(ProcessorErrorMessages.IncorrectMacroParameterValue, currentParameter));
					}

					var variable = VariablesStorage.Entities.FirstOrDefault(e => e.Name.EqualsIgnoreCase(vals[1]));
					//if (variable == null)
					//{
					//	throw new CustomException(string.Format(ProcessorErrorMessages.IncorrectMacroParameterValue, currentParameter));
					//}
					if (variable != null && variable.Value == null)
					{
						throw new CustomException(string.Format(ProcessorErrorMessages.MacroParameterIsEmptyVariable, currentParameter));
					}
					value = variable?.Value ?? passedValue;

					if (dict.Keys.Contains(vals[0]))
					{
						throw new CustomException(string.Format(ProcessorErrorMessages.DublicateMacroCallParameter, vals[0]));
					}
					macroParameter.Value = value;
					// macro.Parameters[i].Value = value;
					dict.Add(vals[0], value);
				}
			};

			// cleaning
			//if(macro != null && macro.Parameters!= null && macro.Parameters.Count > 0)
   //         {
			//	foreach(var parameter in macro.Parameters)
   //             {
			//		parameter.Value = null;
			//		parameter.DefaultValue = null;
			//	}
   //         }

			ParsePositionedParams();
			var parametersWithoutValue = MacrosStorage.CurrentMacro.Parameters.Where(p => p.Type == MacroParameterTypes.Position && p.Value == null).Select(p=>p.Name);
			if (parametersWithoutValue.Count() > 0)
            {
				throw new CustomException($"Следующим параметрам не присвоено значение: {string.Join(", ", parametersWithoutValue)}");
			}
			ParseKeyParams();
			if(macro.Parameters.Where(p=>p.Type == MacroParameterTypes.Key && String.IsNullOrWhiteSpace(p.Value)).Count() != 0){
				throw new CustomException("Не заданы значения для всех обязательных ключевых параметров");
            }

			var processedBody = macro.Body.Select(e => (CodeEntity)e.Clone()).ToList();

			// замена параметров в макросе на числа
			var macroCount = 0;
			foreach (var sourceLine in processedBody)
			{
				if (sourceLine.Operation == "MACRO") macroCount++;
				if (sourceLine.Operation == "MEND") macroCount--;
				if (macroCount != 0) continue;

				if (sourceLine.Operation.EqualsIgnoreCase("WHILE"))
				{
					if (sourceLine.Operands.Count > 0)
					{
						//foreach (var sign in Helpers.ComparisonSigns)
						//{
						//	var t = sourceLine.Operands[0].Split(new string[] { sign }, StringSplitOptions.None);
						//	if (t.Length == 2)
						//	{
						//		if (macro.Parameters.Any(e => e.Name == t[0].Trim()))
						//		{
						//			t[0] = dict[t[0].Trim()].ToString();
						//		}
						//		if (macro.Parameters.Any(e => e.Name == t[1].Trim()))
						//		{
						//			t[1] = dict[t[1].Trim()].ToString();
						//		}
						//		sourceLine.Operands[0] = t[0] + sign + t[1];
						//		break;
						//	}
						//}
					}
					else
					{
						throw new CustomException(string.Format(ProcessorErrorMessages.IncorrectDirectiveUsage, sourceLine.Operation));
					}
				}
				else if (sourceLine.Operation.In("SET", "INC"))
				{
					//if (MacrosStorage.CurrentMacro ==  null || MacrosStorage.CurrentMacro.Parameters == null) continue;
					//var par = MacrosStorage.CurrentMacro.Parameters.FirstOrDefault(p => p.Name == sourceLine.Operands[0]);
					//if (par != null)
					//{
					//	if (sourceLine.Operation.ToUpper() == "SET")
					//	{
					//		if (sourceLine.Operands.Count != 2)
					//		{
					//			throw new CustomException(string.Format("Плохие аргументы команды"));
					//		}
					//		if (int.TryParse(sourceLine.Operands[1], out int x))
					//		{
					//			par.Value = x;
					//			dict[par.Name] = x;
					//		}
					//		else
					//		{
					//			throw new CustomException(string.Format("Значение дрянь", sourceLine.Operands[1]));
					//		}
					//	}
					//                   else
					//                   {
					//		if (sourceLine.Operands.Count != 1)
					//		{
					//			throw new CustomException(string.Format("Плохие аргументы команды"));
					//		}
					//		par.Value++;
					//		dict[par.Name]++;
					//	}
					//}
					continue;
     //               else
     //               {
					//	throw new CustomException(string.Format("Неизвестный параметр", sourceLine.Operands[0]));
					//}					
				}
				else
				{
					for (var i = 0; i < sourceLine.Operands.Count; i++)
					{
						var currentOperand = sourceLine.Operands[i];
						//if (dict.Keys.Contains(currentOperand))
						//{
						//	sourceLine.Operands[i] = dict[currentOperand].ToString();
						//}

						if (currentOperand.Contains("="))
						{
							var t = currentOperand.Split(new string[] { "=" }, StringSplitOptions.None);
							if (t.Length == 2)
							{
								if (macro.Parameters.Any(e => e.Name == t[1].Trim()))
								{
									t[1] = dict[t[1].Trim()];
								}
								sourceLine.Operands[i] = t[0] + "=" + t[1];
							}
						}						
					}
				}
			}

			return processedBody;
		}
	}


	public static class CheckSourceEntity
	{
		/// <summary>
		/// Проверка на метку (может быть пустая или много двоеточий)
		/// </summary>
		/// <param name="se">строка с операцией меткой</param>
		public static void CheckLabel(CodeEntity se)
		{
			if (se.SourceString.Split(':').Length > 2 && se.Operation != "BYTE")
			{
				throw new CustomException($"{ProcessorErrorMessages.ExtraColonInLine} ({se.SourceString})");
			}
			if (se.SourceString.Split(':').Length > 1 && string.IsNullOrEmpty(se.SourceString.Split(':')[0]))
			{
				throw new CustomException($"{ProcessorErrorMessages.ExtraColonInLine} ({se.SourceString})");
			}
		}

		/// <summary>
		/// Проверка строки с операцией MACRO
		/// </summary>
		/// <param name="se">строка с операцией MACRO</param>
		public static void CheckMacro(CodeEntity se, int macroCount)
		{
			if (se.SourceString.Contains(":"))
			{
				throw new CustomException($"{ProcessorErrorMessages.LabesInMacroDefinition} ({se.SourceString})");
			}
			if (string.IsNullOrEmpty(se.Label) || !Helpers.IsLabel(se.Label, se.IsMacroChild, se.MacroParentName))
			{
				throw new CustomException($"{ProcessorErrorMessages.IncorrectMacroName} ({se.SourceString})");
			}
			if (!se.IsMacroChild)
			{
				if (se.Label.Contains("_"))
				{
					if (MacrosStorage.IsInTMO(se.Label))
					{
						throw new CustomException($"{ProcessorErrorMessages.MacrosAleradyExists} ({se.Label}): {se.SourceString}");
					}
				}
				else
				{
					if(MacrosStorage.Entities.FirstOrDefault(m => m.Name == se.Label) != null)
                    {
						throw new CustomException($"{ProcessorErrorMessages.MacrosAleradyExists} ({se.Label}): {se.SourceString}");
					}
				}
				//if (MacrosStorage.IsInTMO(se.Label))
				//{
				//	throw new CustomException($"{ProcessorErrorMessages.MacrosAleradyExists} ({se.Label}): {se.SourceString}");
				//}
			}
			else
			{
				if (MacrosStorage.IsInTMO(se.Label, se.MacroParentName))
				{
					throw new CustomException($"{ProcessorErrorMessages.MacrosAleradyExists} ({se.Label}): {se.SourceString}");
				}
			}

			Helpers.CheckNames(se.Label, se.IsMacroChild);
		}

		/// <summary>
		/// Проверка строки с операцией MEND
		/// </summary>
		/// <param name="se">строка с операцией MEND</param>
		public static void CheckMend(CodeEntity se, int macroCount)
		{
			if (se.Operands.Count != 0)
			{
				throw new CustomException($"{ProcessorErrorMessages.MendWithParameters} ({se.SourceString})");
			}
			if (!string.IsNullOrEmpty(se.Label))
			{
				throw new CustomException($"{ProcessorErrorMessages.MendWithLabel} ({se.SourceString})");
			}
			if (macroCount == 0)
			{
				throw new CustomException($"{ProcessorErrorMessages.IncorrectMacroMendCount} ({se.SourceString})");
			}
		}

		/// <summary>
		/// Проверка строки с операцией END
		/// </summary>
		/// <param name="se">строка с операцией END</param>
		public static void CheckEnd(CodeEntity se, int macroCount)
		{
			if (macroCount != 0)
			{
				throw new CustomException($"{ProcessorErrorMessages.IncorrectMacroMendCount} ({se.SourceString})");
			}
		}

		/// <summary>
		/// Проверка макроподстановки
		/// </summary>
		public static void CheckMacroSubstitution(CodeEntity se, Macro macro)
		{
			//if (se.Operands.Count != macro.Parameters.Count)
			//{
			//	throw new CustomException($"{ProcessorErrorMessages.IncorrectMacroCallParamsCount} ({se.SourceString})");
			//}
			if (!string.IsNullOrEmpty(se.Label))
			{
				throw new CustomException($"{ProcessorErrorMessages.MacroCallWithLabel} ({se.SourceString})");
			}

            //if (se.Operands.Count != 0)
            //{
            //    throw new CustomException("Вызов макроса с параметрами запрещен!");
            //}
           

            //ToDo: убрать проверку
        }

		/// <summary>
		/// Проверка строки с операцией Directives.Variable
		/// </summary>
		/// <param name="se">строка с операцией Directives.Variable</param>
		public static void CheckVariable(CodeEntity se)
		{
			if (se.Operands.Count > 0 && VariablesStorage.IsInVariablesStorage(se.Operands[0]))
			{
				throw new CustomException($"{ProcessorErrorMessages.VariableAleradyExists} ({se.SourceString})");
			}
			if (!String.IsNullOrEmpty(se.Label))
			{
				throw new CustomException($"{ProcessorErrorMessages.VariableDefinitionWithLabel} ({se.SourceString})");
			}
			if (se.Operands.Count == 2)
			{
				if (!Helpers.IsLabel(se.Operands[0]))
				{
					throw new CustomException($"{ProcessorErrorMessages.IncorrectVariableName} ({se.SourceString})");
				}
				int temp;
				if (Int32.TryParse(se.Operands[1], out temp) == false)
				{
					throw new CustomException($"{ProcessorErrorMessages.IncorrectVariableValue} ({se.SourceString})");
				}
			}
			else if (se.Operands.Count == 1)
			{
				if (!Helpers.IsLabel(se.Operands[0]))
				{
					throw new CustomException($"{ProcessorErrorMessages.IncorrectVariableName} ({se.SourceString})");
				}
			}
			else
			{
				throw new CustomException($"{ProcessorErrorMessages.VariableIncorrectOperandsCount} ({se.SourceString})");
			}
			Helpers.CheckNames(se.Operands[0]);
		}

		/// <summary>
		/// Проверка строки с операцией SET
		/// </summary>
		/// <param name="se">строка с операцией SET</param>
		public static void CheckSet(CodeEntity se, Macro te, out bool foundInVar)
		{
			foundInVar = true;
			if (!string.IsNullOrEmpty(se.Label))
			{
				throw new CustomException($"{ProcessorErrorMessages.SetWithLabel} ({se.SourceString})");
			}
			if (se.Operands.Count == 2)
			{
				if (!te.IsRootMacro && te.Parameters.FirstOrDefault(p => p.Name == se.Operands[0]) != null)
				{
					var param = te.Parameters.FirstOrDefault(p => p.Name == se.Operands[0]);
					param.Value = se.Operands[1];
					foundInVar = false;
                }
                else
                {
					if (!VariablesStorage.IsInVariablesStorage(se.Operands[0]))
					{
						throw new CustomException($"{ProcessorErrorMessages.IncorrectVariableName} ({se.SourceString})");
					}
					//int temp;
					//if (int.TryParse(se.Operands[1], out temp) == false)
					//{
					//	throw new CustomException($"{ProcessorErrorMessages.IncorrectVariableValue} ({se.SourceString})");
					//}
					//foreach (Dictionary<List<string>, Macro> dict in VariablesStorage.WhileVar)
					//{
					//	if (dict.Keys.First().Contains(se.Operands[0]) && dict.Values.First() != te)
					//	{
					//		throw new CustomException($"{ProcessorErrorMessages.VariableIsLoopCounter} (Переменная: {se.Operands[0]}): {se.SourceString}");
					//	}
					//}
				}				
			}
			else
			{
				throw new CustomException($"{ProcessorErrorMessages.SetIncorrectOperands} ({se.SourceString})");
			}
		}

		/// <summary>
		/// Проверка строки с операцией INC
		/// </summary>
		/// <param name="se">строка с операцией INC</param>
		public static void CheckInc(CodeEntity se, Macro te, out bool foundInVar)
		{
			foundInVar = true;
			if (!string.IsNullOrEmpty(se.Label))
			{
				throw new CustomException($"{ProcessorErrorMessages.IncWithLabel} ({se.SourceString})");
			}
			if (se.Operands.Count != 1)
			{
				throw new CustomException($"{ProcessorErrorMessages.IncIncorrectOperandsCount} ({se.SourceString})");
			}
			if (!te.IsRootMacro &&  te.Parameters.FirstOrDefault(p => p.Name == se.Operands[0]) != null)
			{
				var param = te.Parameters.FirstOrDefault(p => p.Name == se.Operands[0]);
                try
                {
					int number;
					if (param.Value == null) throw new CustomException("Попытка получение параметра без значения");
					number = Int32.Parse(param.Value);
					number += 1;
					param.Value = number.ToString();

					foundInVar = false;
				}
                catch
                {
					throw new CustomException("Значение параметра нельзя инкрементировать");
                }
			
			}
            else
            {
				if (!VariablesStorage.IsInVariablesStorage(se.Operands[0]))
				{
					throw new CustomException($"{ProcessorErrorMessages.IncorrectVariableName} ({se.SourceString})");
				}
				if (VariablesStorage.Find(se.Operands[0]).Value == null)
				{
					throw new CustomException($"{ProcessorErrorMessages.NullVariable} ({se.Operands[0]})");
				}
				//foreach (Dictionary<List<string>, Macro> dict in VariablesStorage.WhileVar)
				//{
				//	if (dict.Keys.First().Contains(se.Operands[0]) && dict.Values.First() != te)
				//	{
				//		throw new CustomException($"{ProcessorErrorMessages.VariableIsLoopCounter} (Переменная: {se.Operands[0]}) {se.SourceString}");
				//	}
				//}
			}		
		}

	}

	public static class CheckBody
	{
		/// <summary>
		/// Проверка макроподстановки
		/// </summary>
		public static void CheckMacroRun(CodeEntity se, Macro parent, Macro child)
		{
			var current = parent;
			var list = new List<Macro>();
			while (current.PreviousMacros != null && current.PreviousMacros.Name != null)
			{
				if (list.Contains(current))
				{
					throw new CustomException(ProcessorErrorMessages.Recursion);
				}
				list.Add(current);
				current = current.PreviousMacros;
			}
            if (child != null)
            {
				if (MacrosStorage.IsInTMO(child.Name) && parent.Name == child.Name)
				{
					throw new CustomException($"{ProcessorErrorMessages.SelfMacroCall} (Макрос: {child.Name})");
				}
				if (MacrosStorage.IsInTMO(child.Name) && !child.ParentMacros.LocalTmo.Contains(child))
				{
					throw new CustomException($"{ProcessorErrorMessages.MacroScopeLocal} (Макрос: {child.Name})");
				}
			}
			
		}
	}
}
