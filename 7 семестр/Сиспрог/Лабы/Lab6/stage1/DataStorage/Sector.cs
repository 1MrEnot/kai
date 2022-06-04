using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4.DataStorage
{
    public class Sector:IObservable<Sector>
    {
        public Sector(string name)
        {
            Name = name;

            TableSymbolicNames.ListChanged += Changed;

            BinCodeLines.ListChanged += Changed;

            tableConfig.ListChanged += Changed;

            TableExtDef.ListChanged += Changed;

            ExtRefs.ListChanged += Changed;
        }
        private void Changed(object sender,ListChangedEventArgs e)
        {
            foreach (var o in observers)
            {
                o.OnChanged();
            }
        }
        internal string Name {  get;private set; }
        internal int AddressCounter {  get; set; }
        internal int StartAddress {  get; set; }
        internal int EndAddress {  get; set; }
        internal int ProgramLength {  get; set; }
        internal bool ProgBodyFlag { get; set; } = false;

        private readonly List<IObserver<Sector>> observers = new List<IObserver<Sector>>();
        internal BindingList<ExtDefLine> TableExtDef { get; } = new BindingList<ExtDefLine>(); // Список внешних имен
        internal BindingList<string> ExtRefs { get; } = new BindingList<string>(); // Список внешних ссылок

        internal BindingList<ConfigLine> tableConfig = new BindingList<ConfigLine>(); // Список строк таблицы настройки
        internal BindingList<SymbolicName> TableSymbolicNames { get; } = new BindingList<SymbolicName>();

        internal BindingList<BinCodeLine> BinCodeLines { get; } = new BindingList<BinCodeLine>(); // Список строк двоичного кода (является представлением textBoxBinCode)
        
        public void Subscribe(IObserver<Sector> observer)
        {
            observers.Add(observer);
        }

        internal bool TedContainsName(string name)
        {
            foreach (ExtDefLine extDefLine in TableExtDef)
                if (extDefLine.ExtDef.Equals(name))
                    return true;
            return false;
        }

        // Проверки
        internal bool TsiContainsName(string name, out SymbolicName symbolicName)
        {
            symbolicName = null;
            foreach (SymbolicName item in TableSymbolicNames)
                if (item.Name.Equals(name))
                {
                    symbolicName = item;
                    return true;
                }
            return false;
        }

    }
}
