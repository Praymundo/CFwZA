using System;
using System.Threading;
using System.Windows.Forms;

namespace CFwZA
{
    static class Program
    {
        static readonly Mutex mutex = new Mutex(true, "{578DEA6E-7143-4AC8-A05D-2C176236E43A}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Wrappers.NativeMethods.AllocConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (FrmMain frmMain = new FrmMain())
                {
                    Application.Run(frmMain);
                }
            }
            else
            {
                // send our Win32 message to make the currently running instance
                // jump on top of all the other windows
                Wrappers.NativeMethods.PostMessage(
                    (IntPtr)Wrappers.NativeMethods.HWND_BROADCAST,
                    Wrappers.NativeMethods.WM_SHOWME,
                    IntPtr.Zero,
                    IntPtr.Zero);
            }
        }
    }
}
