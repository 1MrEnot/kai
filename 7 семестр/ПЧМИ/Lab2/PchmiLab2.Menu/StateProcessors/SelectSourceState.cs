namespace PchmiLab2.Menu.StateProcessors
{
    using System;
    using System.IO;

    public class SelectSourceState : SelectFolderState
    {
        public SelectSourceState(App app) : base(app)
        {
        }

        protected override void OnApply()
        {
            App.DirecotryToCopy = new DirectoryInfo(App.CurrentDir);
            App.State = new SelectDestinationState(App);
            Console.WriteLine("Выбрана папка для копирования");
        }

        protected override string ApplyStringText =>
            $"{ApplyStr} - выбрать текущую папку для копирования";
    }
}