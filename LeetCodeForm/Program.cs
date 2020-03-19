using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace LeetCodeForm
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LeetCode407(JsonSerializer.Deserialize<int[][]>(File.ReadAllText("407.json"))));
        }
    }
}
