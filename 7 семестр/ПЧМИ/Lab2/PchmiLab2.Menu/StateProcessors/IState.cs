namespace PchmiLab2.Menu.StateProcessors
{
    public interface IState
    {
        void ProcessMessage(string message);

        string Help { get; }
    }
}