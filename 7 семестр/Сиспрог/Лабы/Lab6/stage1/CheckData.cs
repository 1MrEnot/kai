using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab4
{
    class CheckData
    {
        private static readonly string[] regs = { "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9",
                             "R10", "R11", "R12", "R13", "R14", "R15" };

        private static readonly string[] system_directives = { "START", "END", "BYTE", "WORD", "RESB", "RESW","CSECT","EXTDEF","EXTREF" };

        public static int RegisterNumber(string str)
        {
            return Array.IndexOf(regs, str.ToUpper());
        }

        public static bool IsHexString(string s)
        {
            return Regex.IsMatch(s, "^X\"[a-zA-Z0-9]+\"$");
        }

        public static bool IsLetterString(string s)
        {
            return Regex.IsMatch(s, "^C\".+\"$");
        }

        public static bool IsLabel(string s)
        {
            return Regex.IsMatch(s, "^[a-zA-Z_][a-zA-Z_0-9]+$");
        }
        public static bool isRelative(string s)
        {
            return Regex.IsMatch(s, "^\\[[a-zA-Z0-9_]+\\]$");
        }

        public static bool IsRegister(string str)
        {
            return Array.IndexOf(regs, str.ToUpper()) != -1;
        }

        public static bool IsNumber(string str)
        {
            return str.All(x => char.IsDigit(x));
        }

        public static bool IsLetters(string str)
        {
            return str.All(x => x >= 'a' && x <= 'z' || x >= 'A' && x <= 'Z');
        }

        public static bool IsLetterDigit(string str)
        {
            return str.All(x => x >= 'a' && x <= 'z' || x >= 'A' && x<= 'Z' || x>='0' && x <= '9');
        }

        public static bool IsLetterDigitSpecial(string str)
        {
            return str.ToUpper().All(x => x >= 'a' && x <= 'z' || x >= 'A' && x <= 'Z' || x >= '0' && x <= '9' || "* \"?!/".Contains(x));
        }

        //проверяет соответствует ли переданная строка какой-либо директиве
        public static bool IsDirective(string name)
        {
            return Array.IndexOf(system_directives, name.ToUpper()) > -1;
        }

        public static bool IsAddress(string str)
        {
            return str.All(x => char.IsDigit(x) || "ABCDEF".Contains(x));
        }

        public static string String(string Operand1)
        {
            if(Regex.IsMatch(Operand1, "C\".+\""))
            {
                return Operand1.Substring(2, Operand1.Length - 3);
            }
            return "";
        }

        public static string ByteString(string Operand1)
        {
            if (Regex.IsMatch(Operand1, "X\"[0-9A-F]+\""))
            {
                return Operand1.Substring(2, Operand1.Length - 3);
            }
            return "";
        }

    }
}
