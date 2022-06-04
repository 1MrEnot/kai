namespace Sys
{
    using System;
    using System.Linq;

    internal static class CheckHelper
    {
        public static readonly char[] Hex =
        {
            'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        private static readonly string[] Reg =
        {
            "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9",
            "R10", "R11", "R12", "R13", "R14", "R15"
        };

        private static readonly char[] Numbers =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        private static readonly char[] Letters =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private static readonly char[] NumbersAndLetters =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static int RegisterNumber(string str)
        {
            return Array.IndexOf(Reg, str.ToUpper());
        }

        public static bool OnlyRegisters(string str)
        {
            str = str.ToUpper();
            for (var i = 0; i < Reg.Length - 1; i++)
            {
                if (Array.IndexOf(Reg, str) == -1)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool OnlyNumbers(string str)
        {
            return str.All(t => Array.IndexOf(Numbers, t) != -1);
        }

        public static bool OnlySymbols(string str)
        {
            return str
                .ToUpper()
                .All(t => Array.IndexOf(Letters, t) != -1);
        }

        public static bool OnlySymbolsAndNumbers(string str)
        {
            return str
                .ToUpper()
                .All(t => Array.IndexOf(NumbersAndLetters, t) != -1);
        }

        //проверяет соответствует ли переданная строка какой-либо директиве
        public static bool IsDirective(string name)
        {
            string[] systemDirectives = { "START", "END", "BYTE", "WORD", "RESB", "RESW" };

            if (Array.IndexOf(systemDirectives, name.ToUpper()) > -1)
            {
                return true;
            }

            return false;
        }

        public static bool IsAdressPossible(string str)
        {
            if (str.Length <= 0)
            {
                return false;
            }

            return str
                .ToUpper()
                .All(t => Array.IndexOf(Hex, t) != -1);
        }

        public static string String(string operand1)
        {
            if (operand1.Length > 3 && operand1[0] == 'C' && operand1[1] == '"' && operand1[operand1.Length - 1] == '"')
            {
                return operand1.Substring(2, operand1.Length - 3);
            }

            return "";
        }

        public static string ByteString(string operand)
        {
            if (operand.Length <= 3 ||
                operand[0] != 'X' ||
                operand[1] != '"' ||
                operand[operand.Length - 1] != '"')
            {
                return "";
            }

            var text = operand.Substring(2, operand.Length - 3);
            return !IsAdressPossible(text) ? "" : text;
        }
    }
}