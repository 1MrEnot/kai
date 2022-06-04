using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DataStorage
{
    internal class ExtDefLine
    {
        public string Address { get; set; }
        public string ExtDef { get; set; }
        public ExtDefLine(string ExtDef)
        {
            this.ExtDef = ExtDef;
            this.Address = "";
        }
        public ExtDefLine(string Address, string ExtDef)
        {
            this.Address = Address;
            this.ExtDef = ExtDef;
        }
        public void SetAddress(string Address)
        {
            this.Address = Address;
        }
    }
}
