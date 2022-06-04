namespace PchmiLab2.Cons
{
    using System;
    using Core;

    public class App
    {
        private readonly CommandFactory _commandFactory;
        private readonly LabApplication _labApplication;

        public App()
        {
            _labApplication = new LabApplication();
            _commandFactory = new CommandFactory(_labApplication);

            _labApplication.DirChangedNotify += OnDirChanged;
            _labApplication.DirMoveFailedNotify += OnDirMovingException;
            _labApplication.DirMovedNotify += OnDirMoved;
        }

        public void Run()
        {
            OnDirChanged(_labApplication.CurrentDir);
            while (_labApplication.IsRunning)
            {
                var line = Console.ReadLine();
                if (_commandFactory.TryParseCommand(line, out var command))
                {
                    command.Execute();
                }
                else
                {
                    Console.WriteLine("Команда не распознана");
                }
            }
            Console.Write("Нажмите любую клавишу для закрытия окна");
            Console.Read();
        }

        private static void OnDirChanged(string currentDir)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(currentDir);
            Console.ResetColor();
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