using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DataStorage
{
    internal class FormatedString
    {
        internal static string[] Decoded(string str)
        {
            return str.Split('|').Select(x => x.Trim()).ToArray();
        }
        internal static string Coded(string[] arr)
        {
            return string.Join("|",arr.Select(x => x.Trim()));
        }
    }
}