using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DataStorage
{
    internal class ConfigLine
    {
        public string Address { get; set; }
        public string ExtRef { get; set; }
        public ConfigLine(string Address)
        {
            this.Address = Address;
            this.ExtRef = "";
        }
        public ConfigLine(string Address, string ExtRef)
        {
            this.Address = Address;
            this.ExtRef = ExtRef;
        }
    }
}
