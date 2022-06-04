using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemProgramming1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Environment Класс Предоставляет сведения о текущей среде и платформе,
            //а также необходимые для управления ими средства.

            //OSVersion Возвращает объект OperatingSystem, 
            //который содержит идентификатор текущей платформы и номер версии.
            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
