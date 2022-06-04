using System;
using System.Collections.Generic;

namespace SystemSoftware.MacroProcessor
{
    /// <summary>
    /// Объект, представляющий строку исходного кода.
    /// </summary>
    public class CodeEntity : ICloneable
    {
        /// <summary>
		/// Строка как string, как в исходниках.
		/// </summary>
        public string SourceString { get; set; }

        /// <summary>
        /// Родитель для исходных строк.
        /// </summary>
        public Processor Sources { get; set; }

        /// <summary>
        /// Метка.
        /// </summary>
        public string Label { get; set; }

        public string OldLabel { get; set; }

        /// <summary>
        /// Операция.
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// Операнды.
        /// </summary>
        public List<string> Operands { get; set; } = new List<string>();

        /// <summary>
        /// Подозрительна ли строка на макровызов.
        /// </summary>
        public bool IsRemove { get; set; }

        /// <summary>
        /// Номер вызова, чтобы знать, что заменять при макровызове.
        /// </summary>
        public int CallNumber { get; set; }
        public bool IsMacroChild { get; set; }
        public string MacroParentName { get; set; }

        /// <summary>
        /// Представление объекта в виде строки.
        /// </summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString()
        {
            var temp = "";
            if (!string.IsNullOrEmpty(Label))
            {
                if (IsMacroChild)
                {
                    var index = Label.IndexOf("_");
                    if (index > -1)
                    {
                        temp += Label.Remove(index) + ": ";
                    }
                    else
                    {
                        temp += Label + ": ";
                    }
                }
                else
                {
                    temp += Label + ": ";
                }
            }
            temp += Operation;

            foreach (var op in Operands)
            {
                temp += " " + op;
            }
            return temp;
        }
        
        /// <summary>
        /// Клонировать объект.
        /// </summary>
        /// <returns>Клон объекта.</returns>
        public object Clone()
        {
            var clone = (CodeEntity)this.MemberwiseClone();
            clone.Operands = new List<string>(Operands);
            return clone;
        }
    }
}
