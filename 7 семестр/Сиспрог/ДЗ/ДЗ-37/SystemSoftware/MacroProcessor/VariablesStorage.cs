using System.Collections.Generic;
using System.Linq;

namespace SystemSoftware.MacroProcessor
{
    /// <summary>
    /// Хранилище переменных, используемых в программе.
    /// </summary>
    /// 

    //public class MacroParameter
    //{
    //    public string MacrosName { get; set; }
    //    public string ParameterName { get; set; }
    //    public string Value { get; set; }
    //}

    public static class VariablesStorage
    {
        /// <summary>
        /// Сами переменные.
        /// </summary>
        public static List<Variable> Entities { get; set; } = new List<Variable>();

        /// <summary>
        /// Участие переменных в While.
        /// </summary>
        public static Stack<Dictionary<List<string>, Macro>> WhileVar { get; set; } = new Stack<Dictionary<List<string>, Macro>>();


        public static List<MacroParameter> MacroParameters { get; set; } = new List<MacroParameter>();

        /// <summary>
        /// Обновить список переменных.
        /// </summary>
        public static void Refresh()
        {
            Entities = new List<Variable>();
            WhileVar = new Stack<Dictionary<List<string>, Macro>>();
            MacroParameters= new List<MacroParameter>();
        }

        /// <summary>
        /// Найти переменную в списке по имени.
        /// </summary>
        /// <param name="name">Имя переменной для поиска.</param>
        /// <returns>Найденная переменная или null.</returns>
        public static Variable Find(string name)
        {
            return Entities.SingleOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Описана ли уже эта переменная.
        /// </summary>
        /// <param name="name">Переменная для проверки.</param>
        /// <returns>Флаг, описана ли уже эта переменная.</returns>
        public static bool IsInVariablesStorage(string name)
        {
            return Find(name) != null;
        }

        //public static MacroParameter GetParameter(string macrosName, string parameterName)
        //{
        //    return MacroParameters.FirstOrDefault(p => p.MacrosName == macrosName && p.ParameterName == parameterName);
        //}
    }
}
