using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DataStorage
{
    internal class SymbolicName // Строка таблицы символьных имен
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Link { get; set; }
        public int Type { get; set; }
        public SymbolicName(string Name, string Address, string Link)
        {
            this.Name = Name;
            this.Address = Address;
            this.Link = Link;
            this.Type = 0;
        }
        public SymbolicName(string Name, string Address, string Link, int Type)
        {
            this.Name = Name;
            this.Address = Address;
            this.Link = Link;
            this.Type = Type;
        }

        public SymbolicName(string Name, string Address)
        {
            this.Name = Name;
            this.Address = Address;
            this.Link = "";
            this.Type = 0;
        }
    }
}
