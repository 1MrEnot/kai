namespace PchmiLab2.Cons.Commands
{
    using System.Collections.Generic;
    using System.IO;
    using Core;

    public class CopyDirsCommand : BaseCommand
    {
        private readonly string _dest;
        private readonly IEnumerable<string> _filenames;

        public CopyDirsCommand(LabApplication app, string dest, IEnumerable<string> filenames) : base(app)
        {
            _dest = dest;
            _filenames = filenames;
        }

        public override void Execute()
        {
            var destFolder = Path.Join(App.CurrentDir, _dest);
            App.CopyDirs(destFolder, _filenames);
        }
    }
}