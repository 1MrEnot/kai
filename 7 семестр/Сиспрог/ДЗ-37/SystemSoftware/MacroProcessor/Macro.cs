using System;
using System.Collections.Generic;
using System.Linq;
using SystemSoftware.Common;
using SystemSoftware.Resources;

namespace SystemSoftware.MacroProcessor
{
	/// <summary>
	/// Класс, описывающий макрос.
	/// </summary>
	public class Macro
	{
		/// <summary>
		/// Имя макроса.
		/// </summary>
		public string Name { get; set; }
		public string GetRealName()
		{
			if(Name != null)
            {
				var name = Name;
				var index = Name.IndexOf("_");
				if (index > -1)
				{
					name = name.Remove(index);
				}
				return name;
			}
			return null;
		}
		
		public List<CodeEntity> CodeLinesToCheck = new List<CodeEntity>();

		/// <summary>
		/// Тело макроса.
		/// </summary>
		public List<CodeEntity> Body { get; set; } = new List<CodeEntity>();

		/// <summary>
		/// Параметры.
		/// </summary>
		public List<MacroParameter> Parameters { get; set; }

		public int KeyParametersCount => Parameters.Where(p=>p.Type == MacroParameterTypes.Key).Count();
		public int NonKeyParametersCount => Parameters.Where(p=> p.Type == MacroParameterTypes.Position).Count();

		/// <summary>
		/// Первый ключевой параметр.
		/// </summary>
		public int FirstKeyParameterIdx =>
			Array.IndexOf(Parameters?.ToArray() ?? new MacroParameter[] { }, Parameters?.FirstOrDefault(e => e.Type == MacroParameterTypes.Key));


		/// <summary>
		/// Список меток, используемых при AGO.
		/// </summary>
		public List<string> AgoLabels { get; set; } = new List<string>();

		/// <summary>
		/// При обработке вложенных макроописаний нобходимы макрос-родитель и макросы-дети.
		/// </summary>
		public Macro ParentMacros { get; set; }

		/// <summary>
		/// При обработке вложенных макроописаний необходимы макросы-дети.
		/// </summary>
		public List<Macro> ChildrenMacros { get; set; } = new List<Macro>();

		/// <summary>
		/// Локальная область видимости макросов.
		/// </summary>
		public List<Macro> LocalTmo { get; set; } = new List<Macro>();

		/// <summary>
		/// При обработке перекрестных ссылок необходим параметр, определяющий макрос, из которого бы вызван данный макрос.
		/// </summary>
		public Macro PreviousMacros { get; set; }

		/// <summary>
		/// Является ли макрос корневым - телом основной программы.
		/// </summary>
		public bool IsRootMacro { get; set; }

		/// <summary>
		/// Количество итераций для того, чтобы считать цикл бесконечным.
		/// </summary>
		public const int INFINITE_LOOP_COUNT = 500;

		/// <summary>
		/// счетчик уникальных меток
		/// </summary>
		public int UniqueLableForLoop = 1;

		/// <summary>
		/// Для уникальных меток (Метки внутри макроса - да).
		/// </summary>
		private int _uniqueLabelCounter { get; set; }

		/// <summary>
		/// Количество вызовов этого макроса.
		/// </summary>
		private int _counter { get; set; }

		/// <summary>
		/// Макрогенерация.
		/// </summary>
		/// <param name="source">Тело макроса.</param>
		/// <returns>Результирующий ассемблерный код.</returns>
		public Processor CallMacro(List<CodeEntity> source)
		{
			if (Config.FORBID_CALL_BEFORE_DEFENITION)
			{
				// Ругаемся на не опережающее определение макроса
				var realName = GetRealName();

				var firstCallIndex = ParentMacros.Body.FindIndex(c => c.Operation == realName);
				var declarationIndex = ParentMacros.Body.FindIndex(c => c.Label == Name);
				
				if (firstCallIndex < declarationIndex)
					throw new CustomException($"Макрос {GetRealName()} был определён после вызова");
			}
			
			var mainWhile = -1;
			var level = 0;
			var currentWhile1 = new WhileItem();
			var Whiles = new List<WhileItem>();
			var Stack = new Stack<WhileItem>();			

			var macroSourceCode = new Processor(source);

			// проверки
			CheckMacros.CheckMacroLabels(this);
			CheckMacros.CheckLocalTmo();
			var macroCount = 0;

			
			#region while, if, go...

			// исполнять ли команду дальше
			var runStack = new Stack<bool>();
			runStack.Push(true);
			// стек комманд, появлявшихся ранее
			var commandStack = new Stack<ConditionalDirective>();
			// стек строк, куда надо вернуться при while
			var whileStack = new Stack<int>();

			for (var i = 0; i < macroSourceCode.SourceCodeLines.Count; i++)
			{
				var current = macroSourceCode.SourceCodeLines[i].Clone() as CodeEntity;
				if (current.Operation == "WHILE")
				{
					var a = new WhileItem { StartIndex = i, Count = 0, Level = level++ };
					Whiles.Add(a);
					Stack.Push(a);
				}

				if (current.Operation == "ENDW")
				{
					var a = Stack.Pop();
					a.EndIndex = i;
					level--;
				}
			}

			if (Whiles.Count > 0)
			{
				mainWhile = Whiles[0].StartIndex;
				currentWhile1 = Whiles[0];
			}

			if (Whiles.Count > 0) currentWhile1 = Whiles[0];

			for (var i = 0; i < macroSourceCode.SourceCodeLines.Count; i++)
			{				
				var current = macroSourceCode.SourceCodeLines[i].Clone() as CodeEntity;

				var t = Whiles.FirstOrDefault(tp => tp.StartIndex == i);

				if (t != null && currentWhile1?.StartIndex != i)
				{
					var whiles = Whiles.Where(tp => tp.Level - 1 == t.Level);
					foreach (var w in whiles)
					{
						w.Count = 0;
					}
					currentWhile1 = t;
					//currentWhile1.Count = 0;
				}

				//_counter++;
				if (currentWhile1.Count == INFINITE_LOOP_COUNT)
				{
					throw new CustomException(ProcessorErrorMessages.InfiniteLoop);
				}

				// Вложенные макросы не обрабатываем
				if (current.Operation == "MACRO")
				{
					macroCount++;
				}
				else if (current.Operation == "MEND")
				{
					macroCount--;
				}
				if (macroCount == 0)
				{
					CheckMacros.CheckInner(current, commandStack);
/*					if (current.Operation == "IF")
					{
						CheckMacros.CheckIf(this);
						commandStack.Push(ConditionalDirective.IF);						
						runStack.Push(runStack.Peek() && Helpers.Compare(current.Operands[0], this));
						continue;
					}
					if (current.Operation == "ELSE")
					{
						CheckMacros.CheckIf(this);
						commandStack.Pop();
						commandStack.Push(ConditionalDirective.ELSE);
						var elseFlag = runStack.Pop();
						runStack.Push(runStack.Peek() && !elseFlag);
						continue;
					}
					if (current.Operation == "ENDIF")
					{
						CheckMacros.CheckIf(this);
						commandStack.Pop();
						runStack.Pop();
						continue;
					}*/
/*					if (current.Operation == "WHILE")
					{
						MemoryManager.CheckMemory();
						currentWhile1.Count++;
						CheckMacros.CheckWhile(this);
						if (current.Operands.Count == 1)
							Helpers.PushConditionArgs(current.Operands[0], this);
						commandStack.Push(ConditionalDirective.ENDIF);
						runStack.Push(runStack.Peek() && Helpers.Compare(current.Operands[0], this));
						whileStack.Push(i);
						continue;
					}
					if (current.Operation == "ENDW")
					{						
						CheckMacros.CheckWhile(this);
						commandStack.Pop();
						var newI = whileStack.Pop() - 1;
						if (runStack.Pop())
						{
							i = newI;
						}
						continue;
					}*/
					
/*					if (current.Operation.In("AIF", "AGO"))
					{
						// If AIF condition is false, skip current line
						CheckMacros.CheckAif(this);
						if (current.Operation == "AIF" && !Helpers.Compare(current.Operands[0], this))
						{
							continue;
						}

						if (runStack.Peek())
						{
							var label = current.Operation == "AIF" ? current.Operands[1] : current.Operands[0];
							// находим метку, чтобы туда прыгнуть
							var ready = false;
							var agoStack = new Stack<bool>();

							// вверх
							var localMacroCount = 0;
							for (var j = i; j >= 0; j--)
							{
								// Вложенные макросы не смотрим
								if (macroSourceCode.SourceCodeLines[j].Operation == "MACRO") localMacroCount++;
								if (macroSourceCode.SourceCodeLines[j].Operation == "MEND") localMacroCount--;
								if (localMacroCount != 0) continue;

								if (macroSourceCode.SourceCodeLines[j].Operation == "IF" || macroSourceCode.SourceCodeLines[j].Operation == "WHILE")
								{
									if (agoStack.Count > 0)
									{
										agoStack.Pop();
									}
								}
								if (macroSourceCode.SourceCodeLines[j].Operation == "ELSE")
								{
									if (agoStack.Count > 0)
									{
										agoStack.Pop();
									}
									agoStack.Push(false);
								}
								if (macroSourceCode.SourceCodeLines[j].Operation == "ENDIF" || macroSourceCode.SourceCodeLines[j].Operation == "ENDW")
								{
									agoStack.Push(false);
								}
								if (macroSourceCode.SourceCodeLines[j].Label == label && (agoStack.Count == 0 || agoStack.Peek()))
								{
									i = j - 1;
									ready = true;
									break;
								}
							}

							// вниз
							if (!ready)
							{
								localMacroCount = 0;
								for (var j = i; j < macroSourceCode.SourceCodeLines.Count; j++)
								{
									// Вложенные макросы не смотрим
									if (macroSourceCode.SourceCodeLines[j].Operation == "MACRO") localMacroCount++;
									if (macroSourceCode.SourceCodeLines[j].Operation == "MEND") localMacroCount--;
									if (localMacroCount != 0) continue;

									if (macroSourceCode.SourceCodeLines[j].Operation == "IF" || macroSourceCode.SourceCodeLines[j].Operation == "WHILE")
									{
										agoStack.Push(false);
									}
									if (macroSourceCode.SourceCodeLines[j].Operation == "ELSE")
									{
										if (agoStack.Count > 0)
										{
											agoStack.Pop();
										}
										agoStack.Push(false);
									}
									if (macroSourceCode.SourceCodeLines[j].Operation == "ENDIF" || macroSourceCode.SourceCodeLines[j].Operation == "ENDW")
									{
										if (agoStack.Count > 0)
										{
											agoStack.Pop();
										}
									}
									if (macroSourceCode.SourceCodeLines[j].Label == label && (agoStack.Count == 0 || agoStack.Peek()))
									{
										i = j - 1;
										ready = true;
										break;
									}
								}
							}
							if (!ready)
							{
								throw new CustomException("Метка при директиве " + current.Operation + " находится вне зоны видимости или не описана");
							}
						}

						continue;
					}
				*/	
				}
				
                if (runStack.Peek())
				{
					macroSourceCode.FirstRunStep(current, this);
				}
			}			

			#endregion
			
			// Список меток, которые уже найдены
			var markedLabels = new List<string>();
            var labelsToChange = new List<string>();
			macroCount = 0;
			foreach (var se in macroSourceCode.AssemblerCode)
			{
				if (se.Operation == "MACRO") macroCount++;
				if (se.Operation == "MEND") macroCount--;
				if (macroCount != 0) continue;

				if (!string.IsNullOrEmpty(se.Label))
				{
					if (markedLabels.Contains(se.Label))
					{
						throw new CustomException($"{ProcessorErrorMessages.DuplicateLabelInMacro} (Метка {se.Label}, макрос {Name})");
					}
					markedLabels.Add(se.Label);
				}

				// Уберем метку, она больше не нужна
                if (this.AgoLabels.Contains(se.Label))
                {
                    se.Label = null;
                } else if (!string.IsNullOrEmpty(se.Label))
                {
                    //String newLabel = se.Label + '_' + this.Name + '_' + (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds % 10000;
                    se.OldLabel = se.Label;
                    //se.Label = newLabel;
                    se.SourceString = se.ToString();
                    labelsToChange.Add(se.OldLabel);
                }
            }

            foreach (var se in macroSourceCode.AssemblerCode)
            {
                if (se.Operands.Count > 0)
                {
					//if (labelsToChange.Contains(se.Operands[0]))
					//{
					//    string newLabel = macroSourceCode.AssemblerCode.Find(x => x.OldLabel == se.Operands[0]).Label;
					//    se.Operands[0] = newLabel;
					//    se.SourceString = se.ToString();
					//}

					//var label = labelsToChange.FirstOrDefault(l => l.StartsWith($"{se.Operands[0]}_{this.Name}"));
					var label = labelsToChange.FirstOrDefault(l => l.StartsWith($"$_{se.Operands[0]}_{this.Name}_"));
					if (label != null)
                    {
						se.Operands[0] = label;
                    }
                }
            }

			return macroSourceCode;
		}

	}

	public static class CheckMacros
	{
		/// <summary> Проверяет макрос на наличие меток
		/// </summary>
		public static void CheckMacroLabels(Macro te)
		{
			var result = new List<CodeEntity>();

			// Вложенные макросы не обрабатываем
			var macroCount = 0;
			foreach (var se in te.Body)
			{
				if (se.Operation == "MACRO")
				{
					macroCount++;
				}
				else if (se.Operation == "MEND")
				{
					macroCount--;
				}
				if (macroCount == 0)
				{
					result.Add(se.Clone() as CodeEntity);
				}
			}

			var repeadedLabels = result
				.GroupBy(c => c.Label)
				.Where(g => !string.IsNullOrWhiteSpace(g.Key))
				.Where(g => g.Count() >= 2)
				.Select(g => g.Key)
				.ToList();

			if (repeadedLabels.Count != 0)
				throw new CustomException($"Обнаружены повторяющиеся метки внутри макроса {te.GetRealName()}: {string.Join(" ", repeadedLabels)}");

			// Определяем метки, используемые при AGO
			/*          foreach (var se in result)
			          {
			              if (se.Operation == "AGO" && se.Operands.Count > 0 && !te.AgoLabels.Contains(se.Operands[0]))
			              {
			                  te.AgoLabels.Add(se.Operands[0]);
			              }
			              if (se.Operation == "AIF" && se.Operands.Count > 1 && !te.AgoLabels.Contains(se.Operands[1]))
			              {
			                  te.AgoLabels.Add(se.Operands[1]);
			              }
			          }
		  
			          // Список меток, которые являются частью AGO, и уже найдены
			          var markedLabels = new List<string>();
		  
			          foreach (var sourceLine in result)
			          {
			              if (string.IsNullOrEmpty(sourceLine.Label) || sourceLine.Operation.ToUpper() == "MACRO")
			              {
			                  continue;
			              }
		  
			              //if (!te.AgoLabels.Contains(sourceLine.Label))
			              //{
			              //    throw new CustomException(ProcessorErrorMessages.LabelInMacro);
			              //}
			              if (markedLabels.Contains(sourceLine.Label))
			              {
			                  throw new CustomException($"{ProcessorErrorMessages.DuplicateLabelInMacro} (Метка {sourceLine.Label}, макрос {te.Name})");
			              }
			              markedLabels.Add(sourceLine.Label);
			          }
		  
			          // Все ли метки найдены
			          foreach (var agoLabel in te.AgoLabels)
			          {
				          if (!markedLabels.Contains(agoLabel))
				          {
					          throw new CustomException($"{ProcessorErrorMessages.AgoLabelNotFound} (Метка {agoLabel}, макрос {te.Name})");
				          }
			          }*/
		}

		/// <summary>
		/// Локальная область видимости макросов. Из parent можно вызвать только макросы localTMO
		/// </summary>
		public static void CheckLocalTmo()
		{
			foreach (var te in MacrosStorage.Entities)
			{
				te.LocalTmo.Clear();
				var current = te;
				while (current != MacrosStorage.Root)
				{
					te.LocalTmo.AddRange(current.ChildrenMacros);
					current = current.ParentMacros;
				}
				te.LocalTmo.AddRange(current.ChildrenMacros);
				te.LocalTmo.Remove(te);
			}
			MacrosStorage.Root.LocalTmo = MacrosStorage.Root.ChildrenMacros;
		}

		/// <summary>
		/// Проверка макроса на WHILE-ENDW
		/// </summary>
		public static void CheckWhile(Macro te)
		{
			var whileCount = 0;

			// Вложенные макросы не обрабатываем
			var result = new List<CodeEntity>();
			var macroCount = 0;
			foreach (var se in te.Body)
			{
				if (se.Operation == "MACRO")
				{
					macroCount++;
				}
				else if (se.Operation == "MEND")
				{
					macroCount--;
				}
				if (macroCount == 0)
				{
					result.Add(se.Clone() as CodeEntity);
				}
			}

			//проверка корректности WHILE-ENDW
			try
			{
				foreach (var str in result)
				{
					if (str.Operation == "WHILE")
					{
						if (str.Operands.Count != 1)
						{
							throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveOperands} {Directives.While} ({str.SourceString})");
						}
						if (!string.IsNullOrEmpty(str.Label))
						{
							throw new CustomException($"{ProcessorErrorMessages.DirectiveWithLabel_1} {Directives.While} {ProcessorErrorMessages.DirectiveWithLabel_2} ({str.SourceString})");
						}
						whileCount++;
					}
					else if (str.Operation == "ENDW")
					{
						if (str.Operands.Count != 0)
						{
							throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveOperands} {Directives.Endw} ({str.SourceString})");
						}
						if (!string.IsNullOrEmpty(str.Label))
						{
							throw new CustomException($"{ProcessorErrorMessages.DirectiveWithLabel_1} {Directives.Endw} {ProcessorErrorMessages.DirectiveWithLabel_2} ({str.SourceString})");
						}
						whileCount--;
						if (whileCount < 0)
						{
							throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveUsage} {Directives.While}-{Directives.Endw}");
						}
					}
					else if ((str.Operation == "MACRO" || str.Operation == "MEND") && whileCount > 0)
					{
						throw new CustomException(ProcessorErrorMessages.MacroDefinitionInLoop);
					}
					else if (str.Operation == Directives.Variable && whileCount > 0)
					{
						throw new CustomException(ProcessorErrorMessages.VariablesInLoop);
					}
                    else if (!string.IsNullOrEmpty(str.Label) && str.Operation != "MACRO" && whileCount > 0)
                    {
                        throw new CustomException(ProcessorErrorMessages.LabelsInLoop);
                    }
                }

				if (whileCount != 0)
				{
					throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveUsage} {Directives.While}-{Directives.Endw}");
				}
			}
			catch (CustomException ex)
			{
				throw new CustomException(ex.Message);
			}
			catch (Exception)
			{
				throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveUsage} {Directives.While}-{Directives.Endw}");
			}
		}

		public static void CheckAif(Macro te)
		{
			// Вложенные макросы не обрабатываем
			var result = new List<CodeEntity>();
			var macroCount = 0;
			foreach (var se in te.Body)
			{
				if (se.Operation == "MACRO")
				{
					macroCount++;
				}
				else if (se.Operation == "MEND")
				{
					macroCount--;
				}
				if (macroCount == 0)
				{
					result.Add(se.Clone() as CodeEntity);
				}
			}

			try
			{
				foreach (var str in result)
				{
					if (str.Operation == "AIF")
					{
						if (str.Operands.Count != 2)
						{
							throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveOperands} {Directives.Aif} ({str.SourceString})");
						}
						if (!string.IsNullOrEmpty(str.Label))
						{
							throw new CustomException($"{ProcessorErrorMessages.DirectiveWithLabel_1} {Directives.Aif} {ProcessorErrorMessages.DirectiveWithLabel_2} ({str.SourceString})");
						}
						if (!Helpers.IsLabel(str.Operands[1]))
						{
							throw new CustomException($"{ProcessorErrorMessages.DirectiveWithoutLabelToGo_1} {Directives.Aif} {ProcessorErrorMessages.DirectiveWithoutLabelToGo_2} ({str.SourceString})");
						}
					}
					if (str.Operation == "AGO")
					{
						if (str.Operands.Count != 1)
						{
							throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveUsage} {Directives.Ago} ({str.SourceString})");
						}
						if (!string.IsNullOrEmpty(str.Label))
						{
							throw new CustomException($"{ProcessorErrorMessages.DirectiveWithLabel_1} {Directives.Ago} {ProcessorErrorMessages.DirectiveWithLabel_2} ({str.SourceString})");
						}
						if (!Helpers.IsLabel(str.Operands[0]))
						{
							throw new CustomException($"{ProcessorErrorMessages.DirectiveWithoutLabelToGo_1} {Directives.Ago} {ProcessorErrorMessages.DirectiveWithoutLabelToGo_2} ({str.SourceString})");
						}
					}
				}
			}
			catch (CustomException ex)
			{
				throw new CustomException(ex.Message);
			}
			catch (Exception)
			{
				throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveUsage} {Directives.Aif}-{Directives.Ago}");
			}
		}

		
		/// <summary>
		/// Проверка макроса на IF-ELSE-ENDIF
		/// </summary>
		public static void CheckIf(Macro te)
		{
			var stackIfHasElse = new Stack<bool>();

			// Вложенные макросы не обрабатываем
			var result = new List<CodeEntity>();
			var macroCount = 0;
			foreach (var se in te.Body)
			{
				if (se.Operation == "MACRO")
				{
					macroCount++;
				}
				else if (se.Operation == "MEND")
				{
					macroCount--;
				}
				if (macroCount == 0)
				{
					result.Add(se.Clone() as CodeEntity);
				}
			}

			//проверка корректности IF-ELSE-ENDIF
			try
			{
				foreach (var str in result)
				{
					if (str.Operation == "IF")
					{
						if (str.Operands.Count != 1)
						{
							throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveOperands} {Directives.If} ({str.SourceString})");
						}
						if (!string.IsNullOrEmpty(str.Label))
						{
							throw new CustomException($"{ProcessorErrorMessages.DirectiveWithLabel_1} {Directives.If} {ProcessorErrorMessages.DirectiveWithLabel_2} ({str.SourceString})");
						}
						stackIfHasElse.Push(false);
					}
					if (str.Operation == "ELSE")
					{
						if (str.Operands.Count != 0)
						{
							throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveOperands} {Directives.Else} ({str.SourceString})");
						}
						if (!string.IsNullOrEmpty(str.Label))
						{
							throw new CustomException($"{ProcessorErrorMessages.DirectiveWithLabel_1} {Directives.Else} {ProcessorErrorMessages.DirectiveWithLabel_2} ({str.SourceString})");
						}
						if (stackIfHasElse.Peek() == true)
						{
							throw new CustomException($"{ProcessorErrorMessages.ExtraBranch} {Directives.Else} ({str.SourceString})");
						}

						stackIfHasElse.Pop();
						stackIfHasElse.Push(true);
					}
					if (str.Operation == "ENDIF")
					{
						if (str.Operands.Count != 0)
						{
							throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveOperands} {Directives.EndIf}");
						}
						if (!string.IsNullOrEmpty(str.Label))
						{
							throw new CustomException($"{ProcessorErrorMessages.DirectiveWithLabel_1} {Directives.EndIf} {ProcessorErrorMessages.DirectiveWithLabel_2} ({str.SourceString})");
						}
						stackIfHasElse.Pop();
					}
				}

				if (stackIfHasElse.Count > 0)
				{
					throw new CustomException($"{ProcessorErrorMessages.DirectiveMissed} {Directives.EndIf}");
				}
			}
			catch (CustomException ex)
			{
				throw new CustomException(ex.Message);
			}
			catch (Exception)
			{
				throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveUsage} {Directives.If}-{Directives.EndIf}");
			}
		}		

		/// <summary>
		/// Проверка вложенностей
		/// </summary>
		/// <param name="current"></param>
		/// <param name="stack"></param>
		public static void CheckInner(CodeEntity current, Stack<ConditionalDirective> stack)
		{
			if (current.Operation == "IF")
			{
				return;
			}
			if (current.Operation == "ELSE")
			{
				if (stack.Count > 0 && stack.Peek() != ConditionalDirective.IF)
				{
					throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveUsage} {Directives.Else}");
				}
				return;
			}
			if (current.Operation == "ENDIF")
			{
				if (stack.Count > 0 && stack.Peek() != ConditionalDirective.IF && stack.Peek() != ConditionalDirective.ELSE)
				{
					throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveUsage} {Directives.EndIf}");
				}
				return;
			}
			if (current.Operation == "WHILE")
			{
				return;
			}
			if (current.Operation == "ENDW")
			{
				if (stack.Count > 0 && stack.Peek() != ConditionalDirective.ENDIF)
				{
					throw new CustomException($"{ProcessorErrorMessages.IncorrectDirectiveUsage} {Directives.Endw}");
				}
				return;
			}
		}
	}
}
