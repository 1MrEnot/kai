namespace PchmiLab2.Menu.StateProcessors
{
    public class MainMenuState : IState
    {
        private const string HelpString = @"e - выход
0 - копировать
";
        private readonly App _app;

        public MainMenuState(App app)
        {
            _app = app;
        }

        public void ProcessMessage(string message)
        {
            switch (message)
            {
                case "e":
                    _app.IsRunning = false;
                    return;
                case "0":
                    _app.State = new SelectSourceState(_app);
                    return;
            }
        }

        public string Help => HelpString;
    }
}