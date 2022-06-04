namespace PchmiLab2.Cons.Commands
{
    using System;
    using Core;

    public class ChangeDirCommand : BaseCommand
    {
        private readonly string _newDir;

        public ChangeDirCommand(LabApplication app, string newDir) : base(app)
        {
            _newDir = newDir;
        }

        public override void Execute()
        {
            try
            {
                App.CurrentDir = _newDir;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}