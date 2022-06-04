using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class OperationCode // Строка таблицы кодов операций
    {
        public OperationCode(string mKOP, string hexCode, int codeLength)
        {
            MKOP = mKOP;
            HexCode = hexCode;
            CodeLength = codeLength;
        }

        public string MKOP { get; private set; }
        public string HexCode { get; private set; }
        public int CodeLength { get; private set; }

    }
}
