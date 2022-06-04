namespace PchmiLab2.Cons.Commands
{
    using Core;

    public abstract class BaseCommand
    {
        protected LabApplication App { get; }

        protected BaseCommand(LabApplication app)
        {
            App = app;
        }

        public abstract void Execute();
    }
}