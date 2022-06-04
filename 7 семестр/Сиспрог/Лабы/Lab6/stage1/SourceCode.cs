using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab4
{
    public class SourceCode
    {
        public SourceCode(string label, string mkop, string op1, string op2)
        {
            if (string.IsNullOrWhiteSpace(mkop)
                && (!string.IsNullOrWhiteSpace(label)
                || !string.IsNullOrWhiteSpace(op1)
                || !string.IsNullOrWhiteSpace(op2)))
            {
                throw new Exception("МКОП не должен быть пустым");
            }
            if (CheckData.IsDirective(label) || CheckData.IsRegister(label))
            {
                throw new Exception("Поле метки является зарезервированным словом");
            }

            if (!CheckData.IsLetterDigit(mkop))
            {
                throw new Exception("Поле команды является не числовой или символной строкой");
            }

            if (!CheckData.IsLetterDigit(op1) 
                && op1.Length != 0 && !CheckData.IsHexString(op1)
                && !CheckData.IsRegister(op1)
                && !CheckData.IsLetterString(op1)
                && !CheckData.IsLabel(op1)
                && !CheckData.isRelative(op1))
            {
                throw new Exception("Поле dir1 не является корректной строкой");
            }

            if (!CheckData.IsLetterDigit(op2) 
                && op2.Length != 0 && !CheckData.IsHexString(op2) 
                && !CheckData.IsLetterString(op2)
                && !CheckData.IsRegister(op2)
                && !CheckData.IsLabel(op2)
                && !CheckData.isRelative(op2))
            {
                throw new Exception("Поле dir2 не является корректной строкой");
            }


            if (label.Length > 0 && !CheckData.IsLetters(Convert.ToString(label[0])))
            {
                //метка должна начинаться с символа
                throw new Exception("Метка должна начинаться с символа");
            }
            Label = label;
            MKOP = mkop;
            Op1 = op1;
            Op2 = op2;
        }

        public string Label { get; set; }
        public string MKOP { get; set; }
        public string Op1 { get; set;  }
        public string Op2 { get; set; }
    }
}
