using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProgramming1
{
    class AddTo
    {
        public static bool TuneTable(string adr, string name, List<List<string>> Tune_table)
        {
            if (adr.Length > 0)
            {   int i;
                for ( i = 0; i < Tune_table.Count;i++)
                    if (Tune_table[i][0]== adr) 
                        return true;
    
                        Tune_table.Add(new List<string>());
                        Tune_table[i].Add(adr);
                        Tune_table[i].Add(name);
               
            }
            return false;
        }

        public static void SupportTable(string n1, string n2, string n3, string n4, ref List<List<string>> Support_table)
        {
            Support_table[0].Add(n1);
            Support_table[1].Add(n2);
            Support_table[2].Add(n3);
            Support_table[3].Add(n4);
        }

    }
}
