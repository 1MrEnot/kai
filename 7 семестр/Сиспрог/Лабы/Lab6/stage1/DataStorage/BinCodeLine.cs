using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DataStorage
{
    internal class BinCodeLine // Строка вспомогательной таблицы
    {
        public string Prefix { get; set; }
        public string Address { get; set; }
        public string Length { get; set; }
        public string Command { get; set; }
        public string Operands { get; set; }
        public BinCodeLine(string Prefix, string Address, string Length, string Command, string Operands)
        {
            this.Prefix = Prefix;
            this.Address = Address;
            this.Length = Length;
            this.Command = Command;
            this.Operands = Operands;
        }
        public BinCodeLine()
        {
            this.Prefix = "";
            this.Address = "";
            this.Length = "";
            this.Command = "";
            this.Operands = "";
        }
        public void FillBinCodeLine(string Prefix, string Address, string Length, string Command, string Operands)
        {
            this.Prefix = Prefix;
            this.Address = Address;
            this.Length = Length;
            this.Command = Command;
            this.Operands = Operands;
        }
    }
}
