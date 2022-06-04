namespace PchmiLab2.Cons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Commands;
    using Core;

    public class CommandFactory
    {
        private static readonly char[] Quotes =
        {
            '\'', '"'
        };
        private readonly LabApplication _labApplication;

        private static readonly Regex CdRegex = new(@"^cd\s+(\S+)$",
            RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex LsRegex = new(@"^ls$",
            RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex CopyRegex = new(@"^copy\s+(\S+)(.+)$",
            RegexOptions.Singleline | RegexOptions.Compiled);

        public CommandFactory(LabApplication labApplication)
        {
            _labApplication = labApplication;
        }

        public bool TryParseCommand(string str, out BaseCommand command)
        {
            str = str.Trim();

            var cdMatch = CdRegex.Match(str);
            if (cdMatch.Success)
            {
                var newFolder = cdMatch.Groups[1].Value;
                command = new ChangeDirCommand(_labApplication, newFolder);
                return true;
            }

            var lsMatch = LsRegex.Match(str);
            if (lsMatch.Success)
            {
                command = new ShowDirCommand(_labApplication);
                return true;
            }

            var copyMatch = CopyRegex.Match(str);
            if (copyMatch.Success)
            {
                var dest = copyMatch.Groups[1].Value;
                var folders = SplitArgs(copyMatch.Groups[2].Value)
                    .Select(a => a.Trim());

                command = new CopyDirsCommand(_labApplication, dest, folders);
                return true;
            }

            command = null;
            return false;
        }

        private IEnumerable<string> SplitArgs(string input)
        {
            var sb = new StringBuilder(input.Length);
            char? quote = null;

            foreach (var c in input.Trim())
            {
                if (quote.HasValue)
                {
                    if (c == quote.Value)
                    {
                        yield return sb.ToString();
                        sb.Clear();
                        quote = null;
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    if (Quotes.Contains(c))
                    {
                        quote = c;
                    }
                    else if (c == ' ' && sb.Length != 0)
                    {
                        yield return sb.ToString();
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }

            yield return sb.ToString();
        }
    }
}