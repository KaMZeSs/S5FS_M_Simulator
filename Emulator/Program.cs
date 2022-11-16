using System.Text;

using Microsoft.VisualBasic.Devices;

namespace Emulator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var openFS = new LoadFS();
            Application.Run(openFS);

            if (openFS.s5FS is not null)
                Application.Run(new MainForm(openFS.s5FS));
        }
    }
}