namespace PchmiLab2.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class LabApplication
    {
        public event DirChangedHandler DirChangedNotify;
        public event FileMoveFailedHandler DirMoveFailedNotify;
        public event FileMovedHandler DirMovedNotify;

        private string _currentDir;

        public bool IsRunning { get; set; }

        public LabApplication()
        {
            _currentDir = Directory.GetCurrentDirectory();
            IsRunning = true;
        }

        public string CurrentDir
        {
            get => _currentDir;
            set
            {
                var newDir = Path.IsPathFullyQualified(value)
                    ? value
                    : Path.GetFullPath(Path.Join(_currentDir, value));
                if (!Directory.Exists(newDir))
                {
                    throw new ArgumentException("Указанной папки не существует");
                }

                _currentDir = newDir;
                DirChangedNotify?.Invoke(_currentDir);
            }
        }

        public IEnumerable<DirectoryInfo> Directories =>
            Directory
                .EnumerateDirectories(_currentDir)
                .Select(p => new DirectoryInfo(p));

        public IEnumerable<FileInfo> Files =>
            Directory
                .EnumerateFiles(_currentDir)
                .Select(p => new FileInfo(p));

        public void CopyDirs(string destFolder, IEnumerable<string> dirs)
        {
            if (!Directory.Exists(destFolder))
            {
                throw new ArgumentException("Указанной папки не существует");
            }

            foreach (var dir in dirs)
            {
                try
                {
                    var source = Path.IsPathFullyQualified(dir)
                        ? dir
                        : Path.Join(_currentDir, dir);
                    var res = Path.Join(destFolder, new DirectoryInfo(dir).Name);
                    DirectoryCopy(source, res);
                    DirMovedNotify?.Invoke(res);
                }
                catch (Exception ex)
                {
                    DirMoveFailedNotify?.Invoke(dir, ex);
                }
            }
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs=true)
        {
            // Get the subdirectories for the specified directory.
            var dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Указанной папки не существует");
            }

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                var subDirs = dir.GetDirectories();
                foreach (var subdir in subDirs)
                {
                    var tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath);
                }
            }
        }
    }

    public delegate void DirChangedHandler(string newDir);

    public delegate void FileMovedHandler(string fileName);

    public delegate void FileMoveFailedHandler(string fileName, Exception exception);
}