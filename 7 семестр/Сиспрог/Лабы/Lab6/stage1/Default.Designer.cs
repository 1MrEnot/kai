﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab4 {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Default {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Default() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Lab4.Default", typeof(Default).Assembly);
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
        ///   Ищет локализованную строку, похожую на PROG|START|0|JMP|L1|
        ///A1|RESB|10|
        ///A2|RESW|20|
        ///B1|WORD|40|
        ///B3|BYTE|X&quot;2F4C008A&quot;|
        ///B4|BYTE|C&quot;Hello!&quot;|
        ///L1|LOADR1|B1|
        ///L2|LOADR2|B4|
        ///|ADD|R1|R2
        ///|SAVER|B1|
        ///|INT|200|
        ///|END||.
        /// </summary>
        internal static string direct {
            get {
                return ResourceManager.GetString("direct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на PROG|START|0|
        ///|JMP|[L1]|
        ///A1|RESB|10|
        ///A2|RESW|20|
        ///B1|WORD|40|
        ///B2|BYTE|X&quot;2F4C008A&quot;|
        ///B3|BYTE|C&quot;Hello!&quot;|
        ///B4|BYTE|128|
        ///L1|LOADR1|B1|
        ///L2|LOADR2|[B4]|
        ///|ADD|R1|R2
        ///|SAVER|[L1]|
        ///|INT|200|
        ///|END||.
        /// </summary>
        internal static string mix {
            get {
                return ResourceManager.GetString("mix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на PROG|START|0|
        ///|EXTDEF|D23|
        ///|EXTDEF|D4|
        ///|EXTREF|D2|
        ///|EXTREF|D21|
        ///D4|RESB|10|
        ///D23|RESB|10|
        ///|JMP|D2|
        ///|SAVER|D21|
        ///|RESB|10|
        ///D2|CSECT||
        ///|EXTDEF|D21|
        ///|EXTREF|D4|
        ///D21|SAVER|D4|
        ///|INT|200|
        ///|END|0|.
        /// </summary>
        internal static string mixed {
            get {
                return ResourceManager.GetString("mixed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на JMP|01|4
        ///LOADR1|02|4
        ///LOADR2|03|4
        ///ADD|04|2
        ///SAVER|05|4
        ///INT|06|2.
        /// </summary>
        internal static string operations {
            get {
                return ResourceManager.GetString("operations", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на PROG|START|0|
        ///|JMP|[L1]|
        ///A1|RESB|10|
        ///A2|RESW|20|
        ///B1|WORD|40|
        ///B3|BYTE|X&quot;2F4C008A&quot;|
        ///B4|BYTE|C&quot;Hello!&quot;|
        ///L1|LOADR1|[B1]|
        ///L2|LOADR2|[B4]|
        ///|ADD|R1|R2
        ///|SAVER|[B1]|
        ///|INT|200|
        ///|END||.
        /// </summary>
        internal static string relative {
            get {
                return ResourceManager.GetString("relative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Program|START|100|
        ///A1|RESB|10|
        ///|JMP|A1||
        ///|LOADR2|B1||
        ///|LOADR1|B1||
        ///B1|WORD|40|
        ///B2|BYTE|X&quot;2F4C008A&quot;|
        ///B3|BYTE|C&quot;Hello!&quot;|
        ///B4|BYTE|12|
        ///L1|LOADR1|B1|
        ///|LOADR2|A1||
        ///|ADD|R1|R2|
        ///|SAVER|B1||
        ///|INT|255||
        ///|END|||.
        /// </summary>
        internal static string source_code {
            get {
                return ResourceManager.GetString("source_code", resourceCulture);
            }
        }
    }
}
