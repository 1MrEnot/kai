namespace PchmiLab2.Menu.StateProcessors
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public abstract class SelectFolderState: IState
    {
        protected readonly App App;
        protected DirectoryInfo[] Directories;
        protected const string ApplyStr = "y";

        protected SelectFolderState(App app)
        {
            App = app;
            Directories = app.Directories.ToArray();
        }

        public void ProcessMessage(string message)
        {
            Directories = App.Directories.ToArray();

            if (message == ApplyStr)
            {
                OnApply();
            }

            if (message == "0")
            {
                App.CurrentDir = new DirectoryInfo(App.CurrentDir).Parent?.FullName ?? App.CurrentDir;
                return;
            }

            if (int.TryParse(message, out var num))
            {
                var dirIndex = num - 1;
                if (Directories.Length < dirIndex + 1 || dirIndex < 0)
                    return;

                var newDir = Directories[dirIndex];
                App.CurrentDir = newDir.FullName;
            }
        }

        public string Help => GetHelp();

        private string GetHelp()
        {
            Directories = App.Directories.ToArray();

            var sb = new StringBuilder();
            sb.AppendLine(App.CurrentDir);
            sb.AppendLine("0 - ..");
            sb.AppendLine(ApplyStringText);

            var i = 1;
            foreach (var directory in Directories)
            {
                sb.AppendLine($"{i} - {directory.Name}");
                i++;
            }

            return sb.ToString();
        }

        protected abstract void OnApply();

        protected abstract string ApplyStringText { get; }
    }
}