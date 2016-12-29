using System;
using System.Runtime.InteropServices;

namespace NCore
{
	public static class NativeMethods
	{
		
		public static bool SetForegroundWindow(IntPtr hWnd)
        {
            //throw new NotImplementedException();
            return false;
        }
		
		public static IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam)
        {
            //throw new NotImplementedException();
            return (IntPtr)0;
        }

        public static bool PostMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam)
        {
            //throw new NotImplementedException();
            return false;
        }

        public static int RegisterWindowMessage(string message)
        {
            //throw new NotImplementedException();
            return 0;
        }
    }
}
