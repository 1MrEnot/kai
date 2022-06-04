using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Lab4.DataStorage;

namespace Lab4
{
    internal partial class Config
    {
        internal static readonly int maxMemoryAdr = 0xffffff;
        internal static readonly string[] registers = { "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R10", "R11", "R12", "R13", "R14", "R15" };

        internal static readonly string[] system_directives = { "START", "END", "BYTE", "WORD", "RESB", "RESW", "CSECT", "EXTDEF", "EXTREF" };

        internal static bool IsDirective(string MKOP)
        {
            return Array.IndexOf(system_directives, MKOP.ToUpper()) > -1;
        }
        
        internal static bool IsRegister(string str)
        {
            return registers.Contains(str.ToUpper());
        }
        
    }
}
