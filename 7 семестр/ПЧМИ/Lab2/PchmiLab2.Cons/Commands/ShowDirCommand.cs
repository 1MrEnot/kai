namespace PchmiLab2.Cons.Commands
{
    using System;
    using System.IO;
    using Core;

    public class ShowDirCommand : BaseCommand
    {
        private const int NameWidth = 32;
        private const int ExtWidth = 7;
        private const int SizeWidth = 4;

        private static readonly string[] SizeUnitNames =
        {
            "b", "Kb", "Mb", "Gb"
        };

        public ShowDirCommand(LabApplication app) : base(app)
        {
        }

        public override void Execute()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var directory in App.Directories)
                    ShowDir(directory);

                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (var file in App.Files)
                    ShowFile(file);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        private static void ShowDir(DirectoryInfo directoryInfo)
        {
            var name = directoryInfo.Name;
            Console.WriteLine(PadName(name));
        }

        private static void ShowFile(FileInfo fileInfo)
        {
            var name = PadName(Path.GetFileNameWithoutExtension(fileInfo.Name));
            var ext = PadExt(fileInfo.Extension);
            var size = GetSizeSting(fileInfo.Length);

            var fullStr = string.Join(' ', name, ext, size);

            Console.WriteLine(fullStr);
        }

        private static string GetSizeSting(long size)
        {
            var currentUnitSize = SizeUnitNames[0];

            foreach (var unitName in SizeUnitNames)
            {
                currentUnitSize = unitName;
                if (size < 1024)
                {
                    break;
                }

                size /= 1024;
            }

            return $"{size.ToString().PadRight(SizeWidth, ' ')}{currentUnitSize}";
        }

        private static string PadName(string name) =>
            Pad(name, NameWidth);

        private static string PadExt(string ext) =>
            Pad(ext, ExtWidth);

        private static string Pad(string str, int width)
        {
            return str.Length > width
                ? str[..width]
                : str.PadRight(width, ' ');
        }
    }
}