namespace PchmiLab2.Menu
{
    using System;
    using System.IO;
    using Core;
    using StateProcessors;

    public class App : LabApplication
    {
        public IState State;
        public DirectoryInfo DirecotryToCopy;

        public App()
        {
            State = new MainMenuState(this);
            DirecotryToCopy = new DirectoryInfo(CurrentDir);
            DirMovedNotify += OnDirMoved;
            DirMoveFailedNotify += OnDirMovingException;
        }

        public void Tick()
        {
            Console.WriteLine(State.Help);
            State.ProcessMessage(Console.ReadLine()?.Trim());
        }

        private static void OnDirMovingException(string dirName, Exception ex)
        {
            Console.WriteLine($@"Ошибка при копировании папки {dirName}: {ex}");
        }

        private static void OnDirMoved(string dirName)
        {
            Console.WriteLine($"Папка скопирована: {dirName}");
        }
    }
}