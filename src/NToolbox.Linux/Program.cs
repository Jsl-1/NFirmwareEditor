using NCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NToolbox.Linux
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += Trace.CurrentDomain_UnhandledExceptionHandler;
            Application.ThreadException += Trace.Application_UnhandledThreadExceptionHandler;


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
