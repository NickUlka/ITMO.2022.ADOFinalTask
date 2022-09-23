using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITMO._2022.ADOFinalTask
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ServerInput input = new ServerInput();
            Application.Run(input);
            if (input.Inputsuccess)
            {
                Application.Run(new Form1());
            }
        }
    }
}
