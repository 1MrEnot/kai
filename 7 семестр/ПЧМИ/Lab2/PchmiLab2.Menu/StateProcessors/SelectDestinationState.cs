namespace PchmiLab2.Menu.StateProcessors
{
    public class SelectDestinationState : SelectFolderState
    {
        public SelectDestinationState(App app) : base(app)
        {
        }

        protected override void OnApply()
        {
            App.CopyDirs(App.CurrentDir, new []{App.DirecotryToCopy.FullName});
            App.State = new MainMenuState(App);
        }

        protected override string ApplyStringText =>
            $"{ApplyStr} - выбрать текущую папку для вставки";
    }
}