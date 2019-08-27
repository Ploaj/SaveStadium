using System;
using System.Windows.Forms;

namespace SaveStadium
{
    static class Program
    {
        public static Main Instance { get; internal set; } 

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Instance = new Main();
            Application.Run(Instance);
        }
    }
}
