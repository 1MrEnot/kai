namespace PchmiLab2.Menu
{
    using System;

    static class Program
    {
        static void Main()
        {
            var labApp = new App();
            while (labApp.IsRunning)
            {
                labApp.Tick();
            }
            Console.WriteLine("Закрытие...");
        }
    }
}