using System.Collections.Generic;
using System.Linq;

namespace SystemSoftware.MacroProcessor
{
    /// <summary>
    /// Таблица макроопределений.
    /// </summary>
    public static class MacrosStorage
    {
        public static Macro CurrentMacro;
        /// <summary>
        /// Список макроопределений.
        /// </summary>
        public static List<Macro> Entities { get; set; } = new List<Macro>();

        /// <summary>
        /// Корневой макрос - тело основной программы.
        /// </summary>
        public static Macro Root { get; } = new Macro
        {
            ParentMacros = null,
            ChildrenMacros = new List<Macro>(),
            IsRootMacro = true
        };

        /// <summary>
        /// Обновить ТМО.
        /// </summary>
        public static void Refresh()
        {
            Entities = new List<Macro>();
        }

        /// <summary>
        /// Поиск макроса в ТМО по имени.
        /// </summary>
        /// <param name="name">Имя макроса.</param>
        /// <returns>Найденный макрос или null.</returns>
        public static Macro SearchInTMO(string name, string parentName = null)
        {                        
            return Entities.FirstOrDefault(x => {                
                if(x.Name != null && !name.Contains("_"))
                {
                    if (parentName == null || parentName == name)
                        return x.GetRealName() == name;
                    return x.GetRealName() == name && parentName == x.ParentMacros.Name;
                }
                if (parentName == null || parentName == name)
                    return x.Name == name;
                return x.Name == name && parentName == x.ParentMacros.Name;
            });
        }

        public static Macro SearchAsChildMacro(string name, string parentName)
        {
            return Entities.FirstOrDefault(x =>
            {
                if (x.Name != null && !name.Contains("_"))
                {                    
                    return x.GetRealName() == name && parentName == x.ParentMacros.Name;
                }
                return x.Name == name && parentName == x.ParentMacros.Name;
            });
        }

        public static Macro SearchAsNotChildMacro(string name)
        {
            return Entities.FirstOrDefault(x => {
                if (x.Name != null && !name.Contains("_"))
                {
                    return x.GetRealName() == name && x.ParentMacros.Name == null;
                }
                return x.Name == name && x.ParentMacros == null;
            });
        }

        public static Macro SearchInTMOAsChildOrNot(string name, string parentName)
        {
            var macro = SearchAsChildMacro(name, parentName);
            if (macro == null)
                macro = SearchAsNotChildMacro(name);
            return macro;
        }

        /// <summary>
        /// Есть ли макрос в ТМО.
        /// </summary>
        /// <param name="name">Имя макроса.</param>
        /// <returns>Флаг, есть ли макрос в ТМО.</returns>
        public static bool IsInTMO(string name)
        {
            return SearchInTMO(name) != null;
        }

        //public static Macro SearchInTMO(string name, string parent)
        //{
        //    return Entities.SingleOrDefault(x => x.Name == name && x.ParentMacros.Name == parent);
        //}

        public static bool IsInTMO(string name, string parent)
        {
            return SearchInTMO(name, parent) != null;
        }

        public static bool IsInTMOAsChildOrNot(string name, string parent)
        {
            return SearchInTMOAsChildOrNot(name, parent) != null;
        }

        public static Macro FindVisibleMacro(Macro current, string name)
        {
            while (true)
            {
                if (current == null || current.Name == null)
                {
                    var macro = SearchAsNotChildMacro(name);
                    return macro;
                }
                else
                {
                    var macro = current.ChildrenMacros.FirstOrDefault(m => m.GetRealName() == name);
                    if (macro != null) return macro;
                }
                current = current.ParentMacros;
            }
        }

        public static List<MacroName> GetMacroNames(Processor sourceCode)
        {
            var res = new List<MacroName>();
            var kostyl = new Dictionary<string, int>();
            foreach (var e in Entities)
            {
                var name = e.Name;
                var index = name.IndexOf("_");
                if (index > -1)
                {
                    name = name.Remove(index);
                }

                var kostylSkip = 0;

                var lines = sourceCode.SourceCodeLines.Where(x =>
                    x.SourceString.ToUpper().Contains($"{name} MACRO".ToUpper()));
                
                if (lines.Any())
                {
                    if (!kostyl.ContainsKey(name))
                    {
                        kostyl.Add(name, 1);
                    }
                    else
                    {
                        kostylSkip = kostyl[name];
                        kostyl[name]++;
                    }

                    var realStart = sourceCode.SourceCodeLines.IndexOf(lines.Skip(kostylSkip).First());
                    var realEnd = realStart + 1 + e.Body?.Count ?? 0;
                    res.Add(new MacroName(name, realStart, realEnd));
                }
            }

            return res;
        }
    }

    public struct MacroName
    {
        public string Name { get; }
        public int Start { get; }
        public int Stop { get; }

        public MacroName(string name, int start, int stop)
        {
            Name = name;
            Start = start;
            Stop = stop;
        }
    }
}
