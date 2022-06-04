namespace Sys
{
    using System.Collections.Generic;

    public class AddTo
    {
        public static bool SettingTable(string adr, List<string> Tune_table)
        {
            if (adr.Length > 0)
            {
                if (Tune_table.FindIndex(x => x == adr) > 0)
                    return false;
                else
                    Tune_table.Add(adr);
            }
            return true;
        }
    }
}