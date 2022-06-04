namespace Sys
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal static class ConverterHelper
    {
        public static string DecToHex(int decNumber)
        {
            return decNumber.ToString("X");
        }

        public static int HexToDec(string hexNumber)
        {
            return int.Parse(hexNumber, System.Globalization.NumberStyles.HexNumber);
        }

        public static string ToSixChars(string number)
        {
            const int lenghtNumber = 6;
            var chrs = number.ToCharArray();
            var sum = new char[lenghtNumber];
            var convertNumber = "";
            if (number == "")
            {
                return "";
            }

            if (chrs.Length <= lenghtNumber)
            {
                var needZero = lenghtNumber - chrs.Length;

                for (var i = lenghtNumber - 1; i >= needZero; i--)
                {
                    sum[i] = chrs[i - needZero];
                }

                for (var i = 0; i < needZero; i++)
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
            var convertNumber = "";
            if (number == "")
            {
                return "";
            }

            if (chrs.Length <= lenghtNumber)
            {
                var needZero = lenghtNumber - chrs.Length;

                for (var i = lenghtNumber - 1; i >= needZero; i--)
                {
                    sum[i] = chrs[i - needZero];
                }

                for (var i = 0; i < needZero; i++)
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

        public static string ToAscii(string str)
        {
            var result = "";
            var textBytes = Encoding.ASCII.GetBytes(str);
            foreach (var textByte in textBytes)
            {
                result = result + DecToHex(Convert.ToInt32(textByte));
            }

            return result;
        }


        public static string EditingString(string symbol, string address, string command, string length,
            string operand1, string operand2)
        {
            var final = "";
            if (symbol == "H")
            {
                final = final + "H";
                final = final + "  " + address;
                final = final + "  " + command;
                final = final + "  " + ToSixChars(DecToHex(Convert.ToInt32(operand1)));
                return final;
            }

            if (symbol == "T")
            {
                final = final + "T" + "  ";
                if (address.Length > 0)
                {
                    final = final + address + "  ";
                }

                final = final + length + "   ";
                final = final + command;

                if (operand1.Length > 0)
                {
                    final = final + operand1;
                }

                if (operand2.Length > 0)
                {
                    final = final + operand2;
                }

                return final;
            }

            if (symbol == "E")
            {
                final = final + "E";
                final = final + "  " + address;

                return final;
            }

            return final;
        }
    }
}