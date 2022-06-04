using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProgramming1
{
    class Converting
    {

        public static string DecToHex(int decNumber)
        {

            string hexNumber = "";
            char[] hexMass = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int tempNumber = decNumber;
            var mod = new List<int>();
            if (tempNumber < 16)
            {
                hexNumber += hexMass[tempNumber];
            }
            else
            {

                while (tempNumber >= 1)
                {
                    tempNumber = tempNumber / 16;
                    mod.Add(decNumber % 16);
                    decNumber = decNumber / 16;


                }

                for (int i = mod.Count - 1; i >= 0; i--)
                {
                    hexNumber += hexMass[mod[i]];
                }
            }

            return hexNumber;
        }

        public static string DecToHexSign(int decNumber)
        {
            uint N = Convert.ToUInt32(4294967295 + decNumber + 1);
            string hexNumber = "";
            char[] hexMass = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int div = decNumber;
            var mod = new List<int>();
            if (div < 16)
            {
                hexNumber += hexMass[div];
            }
            else
            {
                while (div >= 16)
                {
                    div = div / 16;
                    mod.Add(decNumber % 16);
                }
                hexNumber += div;
                for (int i = mod.Count - 1; i >= 0; i--)
                {
                    hexNumber += hexMass[mod[i]];
                }
            }

            return hexNumber;
        }

        public static bool PossibleLabelName(string str)
        {
            char[] numbers = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
                               'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < str.Length; i++)
            {
                if (Array.IndexOf(numbers, str[i]) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static int HexToDec(string hexNumber)
        {
            int decNumber = 0;
            hexNumber = hexNumber.ToUpper();
            var hexNumbers = new List<char>
            {
                '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'
            };

            var chrs = hexNumber.ToCharArray();

            double k = 0;
            for (int i = chrs.Length - 1; i >= 0; i--)
            {

                decNumber += hexNumbers.IndexOf(chrs[i]) * (int)(Math.Pow(16, k));

                k++;
            }

            return decNumber;
        }

        public static string ToSixChars(string number)
        {
            const int lenghtNumber = 6;
            var chrs = number.ToCharArray();
            var sum = new char[lenghtNumber];
            string convertNumber = "";
            if (number == "")
                return "";
            if (chrs.Length <= lenghtNumber)
            {
                int needZero = lenghtNumber - chrs.Length;

                for (int i = lenghtNumber - 1; i >= needZero; i--)
                {
                    sum[i] = chrs[i - needZero];
                }
                for (int i = 0; i < needZero; i++)
                {
                    sum[i] = '0';
                }

                foreach (var s in sum)
                {
                    convertNumber += s;
                }
            }
            return convertNumber;
        }

        public static string ToTwoChars(string number)
        {
            const int lenghtNumber = 2;
            var chrs = number.ToCharArray();
            var sum = new char[lenghtNumber];
            string convertNumber = "";
            if (number == "")
                return "";
            if (chrs.Length <= lenghtNumber)
            {
                int needZero = lenghtNumber - chrs.Length;

                for (int i = lenghtNumber - 1; i >= needZero; i--)
                {
                    sum[i] = chrs[i - needZero];
                }
                for (int i = 0; i < needZero; i++)
                {
                    sum[i] = '0';
                }

                foreach (var s in sum)
                {
                    convertNumber += s;
                }
            }
            return convertNumber;
        }

        public static string ToASCII(string str)
        {
            string result = "";
            byte[] textBytes = Encoding.ASCII.GetBytes(str);
            for (int i = 0; i < textBytes.Length; i++)
            {
                result = result + Converting.DecToHex(Convert.ToInt32(textBytes[i]));
            }
            return result;
        }


        public static string EditingString(string symbol, string address, string command, string length, string operand1, string operand2)
        {
            string final = "";
            if (symbol == "H")
            {
                final = final + "H";
                final = final + "  " + address;
                final = final + "  " + command;
                final = final + "  " + operand1;
                return final;
            }
            else
                if (symbol == "T")
            {
                final = final + "T" + "  ";
                if (address.Length > 0)
                    final = final + address + "  ";
                final = final + length + "   ";
                final = final + command;

                if (operand1.Length > 0)
                    final = final + operand1;
                if (operand2.Length > 0)
                    final = final + operand2;
                return final;
            }
            else
                    if (symbol == "M")
            {
                final = final + "M";
                final = final + "  " + address;

                return final;
            }
            else
                        if (symbol == "E")
            {
                final = final + "E";
                final = final + "  " + address;

                return final;
            }
            else
                            if (symbol == "D")
            {
                final = final + "D";
                final = final + "  " + operand1;
                final = final + "  " + length;

                return final;
            }
            else
                                if (symbol == "R")
            {
                final = final + "R";
                final = final + "  " + operand1;

                return final;
            }
            return final;
        }

        public static string SubHex(string firstNumber, string secondNumber)
        {
            const int lenghtNumber = 6;
            string sub = "";
            var subChars = new char[lenghtNumber];
            var hexNumbers = new List<char>
            {
                '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F',
                 '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E'
            };

            var chrs1 = firstNumber.ToCharArray();
            var chrs2 = secondNumber.ToCharArray();

            bool flag = false;

            if (firstNumber == "" || secondNumber == "")
            {
                return "";
            }
            int chr1;
            for (int i = lenghtNumber - 1; i >= 0; i--)
            {

                if (flag)
                {
                    chr1 = hexNumbers.IndexOf(chrs1[i]) - 1;
                }
                else
                {
                    chr1 = hexNumbers.IndexOf(chrs1[i]);
                }

                if (chr1 >= hexNumbers.IndexOf(chrs2[i]))
                {
                    subChars[i] = hexNumbers[(chr1 - hexNumbers.IndexOf(chrs2[i]))];
                    flag = false;
                }
                else
                {
                    subChars[i] = hexNumbers[(chr1 + 16 - hexNumbers.IndexOf(chrs2[i]))];
                    flag = true;
                }
            }

            foreach (var s in subChars)
            {
                sub += s;
            }
            return sub;
        }
    }
}
