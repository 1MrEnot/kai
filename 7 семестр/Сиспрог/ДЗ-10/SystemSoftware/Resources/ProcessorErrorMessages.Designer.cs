﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SystemSoftware.Resources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ProcessorErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ProcessorErrorMessages() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SystemSoftware.Resources.ProcessorErrorMessages", typeof(ProcessorErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка.
        /// </summary>
        internal static string AgoLabelNotFound {
            get {
                return ResourceManager.GetString("AgoLabelNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Отсутствует директива.
        /// </summary>
        internal static string DirectiveMissed {
            get {
                return ResourceManager.GetString("DirectiveMissed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на При директиве.
        /// </summary>
        internal static string DirectiveWithLabel_1 {
            get {
                return ResourceManager.GetString("DirectiveWithLabel_1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на метки быть не должно.
        /// </summary>
        internal static string DirectiveWithLabel_2 {
            get {
                return ResourceManager.GetString("DirectiveWithLabel_2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на При директиве.
        /// </summary>
        internal static string DirectiveWithoutLabelToGo_1 {
            get {
                return ResourceManager.GetString("DirectiveWithoutLabelToGo_1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на отсутствует метка для ссылки.
        /// </summary>
        internal static string DirectiveWithoutLabelToGo_2 {
            get {
                return ResourceManager.GetString("DirectiveWithoutLabelToGo_2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметр {0} повторяется.
        /// </summary>
        internal static string DublicateMacroCallParameter {
            get {
                return ResourceManager.GetString("DublicateMacroCallParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Повторное описание метки в макросе.
        /// </summary>
        internal static string DuplicateLabelInMacro {
            get {
                return ResourceManager.GetString("DuplicateLabelInMacro", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя параметра повторяется.
        /// </summary>
        internal static string DuplicateMacroParamName {
            get {
                return ResourceManager.GetString("DuplicateMacroParamName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Не инициализированная переменная является частью условия оператора сравнения.
        /// </summary>
        internal static string EmptyVariableInComparison {
            get {
                return ResourceManager.GetString("EmptyVariableInComparison", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка в строке.
        /// </summary>
        internal static string ErrorInLinePrefix {
            get {
                return ResourceManager.GetString("ErrorInLinePrefix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Лишняя ветка.
        /// </summary>
        internal static string ExtraBranch {
            get {
                return ResourceManager.GetString("ExtraBranch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Слишком много двоеточий в строке.
        /// </summary>
        internal static string ExtraColonInLine {
            get {
                return ResourceManager.GetString("ExtraColonInLine", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметры вызова макроса {0} определены некорректно. Разделители между &apos;=&apos;, названием и значением параметра недопустимы. Параметр {1}.
        /// </summary>
        internal static string IcorrectMacroCallKeyParameter {
            get {
                return ResourceManager.GetString("IcorrectMacroCallKeyParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Некорректное количество операндов в директиве инкремента значения переменной.
        /// </summary>
        internal static string IncIncorrectOperandsCount {
            get {
                return ResourceManager.GetString("IncIncorrectOperandsCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Некорректное использование операндов директивы.
        /// </summary>
        internal static string IncorrectDirectiveOperands {
            get {
                return ResourceManager.GetString("IncorrectDirectiveOperands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Некорректное использование директивы.
        /// </summary>
        internal static string IncorrectDirectiveUsage {
            get {
                return ResourceManager.GetString("IncorrectDirectiveUsage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Некорректное задание метки в макросе.
        /// </summary>
        internal static string IncorrectLabelInMacro {
            get {
                return ResourceManager.GetString("IncorrectLabelInMacro", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Макрос {0} вызван с некорректными параметрами.
        /// </summary>
        internal static string IncorrectMacroCallParameters {
            get {
                return ResourceManager.GetString("IncorrectMacroCallParameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Для макроса {0} нужно ввести {2} параметров, а введено: {1}.
        /// </summary>
        internal static string IncorrectMacroCallParametersCount {
            get {
                return ResourceManager.GetString("IncorrectMacroCallParametersCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Количество параметров вызова макроса некорректно.
        /// </summary>
        internal static string IncorrectMacroCallParamsCount {
            get {
                return ResourceManager.GetString("IncorrectMacroCallParamsCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметр {0} макроопределения определен некорректно..
        /// </summary>
        internal static string IncorrectMacroDefinitionParameter {
            get {
                return ResourceManager.GetString("IncorrectMacroDefinitionParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Неверное значение по умолчанию для параметра {0} .
        /// </summary>
        internal static string IncorrectMacroDefinitionParameterDefault {
            get {
                return ResourceManager.GetString("IncorrectMacroDefinitionParameterDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Некорректное макроопределение {0}.
        /// </summary>
        internal static string IncorrectMacroDefinitionParameters {
            get {
                return ResourceManager.GetString("IncorrectMacroDefinitionParameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на MACRO и MEND неодинакового кол-ва.
        /// </summary>
        internal static string IncorrectMacroMendCount {
            get {
                return ResourceManager.GetString("IncorrectMacroMendCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя макроса некорректно.
        /// </summary>
        internal static string IncorrectMacroName {
            get {
                return ResourceManager.GetString("IncorrectMacroName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя параметра {0} некорректно.
        /// </summary>
        internal static string IncorrectMacroParameterName {
            get {
                return ResourceManager.GetString("IncorrectMacroParameterName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметр {0} не является ключевым.
        /// </summary>
        internal static string IncorrectMacroParameterType {
            get {
                return ResourceManager.GetString("IncorrectMacroParameterType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметр {0} имеет некорректное значение.
        /// </summary>
        internal static string IncorrectMacroParameterValue {
            get {
                return ResourceManager.GetString("IncorrectMacroParameterValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя параметра некорректно.
        /// </summary>
        internal static string IncorrectMacroParamName {
            get {
                return ResourceManager.GetString("IncorrectMacroParamName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметры вызова макроса заданы некорректно.
        /// </summary>
        internal static string IncorrectMacroParamsCall {
            get {
                return ResourceManager.GetString("IncorrectMacroParamsCall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Некорректное имя переменной.
        /// </summary>
        internal static string IncorrectVariableName {
            get {
                return ResourceManager.GetString("IncorrectVariableName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Некорректное значение переменной.
        /// </summary>
        internal static string IncorrectVariableValue {
            get {
                return ResourceManager.GetString("IncorrectVariableValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Переменной не присвоено значение.
        /// </summary>
        internal static string IncWithEmptyVariable {
            get {
                return ResourceManager.GetString("IncWithEmptyVariable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Метка при инкременте.
        /// </summary>
        internal static string IncWithLabel {
            get {
                return ResourceManager.GetString("IncWithLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Обнаружен бесконечный цикл.
        /// </summary>
        internal static string InfiniteLoop {
            get {
                return ResourceManager.GetString("InfiniteLoop", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Метки внутри макроса запрещены.
        /// </summary>
        internal static string LabelInMacro {
            get {
                return ResourceManager.GetString("LabelInMacro", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Использование меток в цикле запрещено.
        /// </summary>
        internal static string LabelsInLoop {
            get {
                return ResourceManager.GetString("LabelsInLoop", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на При объявлении макроса не должно быть меток.
        /// </summary>
        internal static string LabesInMacroDefinition {
            get {
                return ResourceManager.GetString("LabesInMacroDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Макровызовы внутри макроса запрещены.
        /// </summary>
        internal static string MacroCallInMacros {
            get {
                return ResourceManager.GetString("MacroCallInMacros", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на При макровызове макроса не должно быть меток.
        /// </summary>
        internal static string MacroCallWithLabel {
            get {
                return ResourceManager.GetString("MacroCallWithLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Вызов макроса не должен содержать параметров.
        /// </summary>
        internal static string MacroCallWithParams {
            get {
                return ResourceManager.GetString("MacroCallWithParams", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Объявление макросов в цикле запрещено.
        /// </summary>
        internal static string MacroDefinitionInLoop {
            get {
                return ResourceManager.GetString("MacroDefinitionInLoop", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Директивы MACRO и MEND не одинакового количества.
        /// </summary>
        internal static string MacroMendCount {
            get {
                return ResourceManager.GetString("MacroMendCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на У макроса не должно быть параметров.
        /// </summary>
        internal static string MacroMustNotHaveParams {
            get {
                return ResourceManager.GetString("MacroMustNotHaveParams", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя параметра {0} дублируется.
        /// </summary>
        internal static string MacroParameterDublicate {
            get {
                return ResourceManager.GetString("MacroParameterDublicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметр {0} - переменная без значения.
        /// </summary>
        internal static string MacroParameterIsEmptyVariable {
            get {
                return ResourceManager.GetString("MacroParameterIsEmptyVariable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя параметра уже используется в другом макросе.
        /// </summary>
        internal static string MacroParamInAnotherMacro {
            get {
                return ResourceManager.GetString("MacroParamInAnotherMacro", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Макрос уже описан.
        /// </summary>
        internal static string MacrosAleradyExists {
            get {
                return ResourceManager.GetString("MacrosAleradyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Макрос не входит в область видимости основной программы.
        /// </summary>
        internal static string MacroScopeGlobal {
            get {
                return ResourceManager.GetString("MacroScopeGlobal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Макрос не входит в область видимости макроса, из которого идет макровызов.
        /// </summary>
        internal static string MacroScopeLocal {
            get {
                return ResourceManager.GetString("MacroScopeLocal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на У макроса не должно быть параметров.
        /// </summary>
        internal static string MacrosWithParameters {
            get {
                return ResourceManager.GetString("MacrosWithParameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Метка у директивы MEND.
        /// </summary>
        internal static string MendWithLabel {
            get {
                return ResourceManager.GetString("MendWithLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметр у директивы MEND.
        /// </summary>
        internal static string MendWithParameters {
            get {
                return ResourceManager.GetString("MendWithParameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя уже используется.
        /// </summary>
        internal static string NameIsAleradyUsed {
            get {
                return ResourceManager.GetString("NameIsAleradyUsed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Переменная не инициализирована.
        /// </summary>
        internal static string NullVariable {
            get {
                return ResourceManager.GetString("NullVariable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметра {0} не существует в макросе.
        /// </summary>
        internal static string ParameterDoesNotExists {
            get {
                return ResourceManager.GetString("ParameterDoesNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Перекрестные ссылки и рекурсия запрещены.
        /// </summary>
        internal static string Recursion {
            get {
                return ResourceManager.GetString("Recursion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Самовызов макроса.
        /// </summary>
        internal static string SelfMacroCall {
            get {
                return ResourceManager.GetString("SelfMacroCall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Некорректное задание переменной.
        /// </summary>
        internal static string SetIncorrectOperands {
            get {
                return ResourceManager.GetString("SetIncorrectOperands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на При задании переменной нельзя использовать метку.
        /// </summary>
        internal static string SetWithLabel {
            get {
                return ResourceManager.GetString("SetWithLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на При сравнении можно использовать только числа и переменные.
        /// </summary>
        internal static string UndefinedComparisonPart {
            get {
                return ResourceManager.GetString("UndefinedComparisonPart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Отсутствие знака сравнения.
        /// </summary>
        internal static string UndefinedComparisonSign {
            get {
                return ResourceManager.GetString("UndefinedComparisonSign", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Вызов неописанного макроса.
        /// </summary>
        internal static string UndefinedMacroCall {
            get {
                return ResourceManager.GetString("UndefinedMacroCall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Использование директивы.
        /// </summary>
        internal static string UseOnlyInsideMacro_1 {
            get {
                return ResourceManager.GetString("UseOnlyInsideMacro_1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на возможно только в теле макроса.
        /// </summary>
        internal static string UseOnlyInsideMacro_2 {
            get {
                return ResourceManager.GetString("UseOnlyInsideMacro_2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Повторное задание переменной.
        /// </summary>
        internal static string VariableAleradyExists {
            get {
                return ResourceManager.GetString("VariableAleradyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Метка при переменной.
        /// </summary>
        internal static string VariableDefinitionWithLabel {
            get {
                return ResourceManager.GetString("VariableDefinitionWithLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Некорректное количество операндов в директиве объявления переменной.
        /// </summary>
        internal static string VariableIncorrectOperandsCount {
            get {
                return ResourceManager.GetString("VariableIncorrectOperandsCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Переменная используется как счетчик в цикле.
        /// </summary>
        internal static string VariableIsLoopCounter {
            get {
                return ResourceManager.GetString("VariableIsLoopCounter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Объявление переменных в цикле запрещено.
        /// </summary>
        internal static string VariablesInLoop {
            get {
                return ResourceManager.GetString("VariablesInLoop", resourceCulture);
            }
        }
    }
}
