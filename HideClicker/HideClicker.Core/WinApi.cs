namespace HideClicker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;


    public static class WinApi
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(IntPtr lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, IntPtr lpszWindow);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Win32Point p);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(ref Win32Point pt);

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Win32Point
    {
        public Int32 X;
        public Int32 Y;
    };

    public enum MouserAction : int
    {
        MouseFIRST = 0x0200,
        MouseMOVE = 0x0200,
        LeftBtnDOWN = 0x0201,
        LeftBtnUP = 0x0202,
        LeftBtnDoubleClick = 0x0203,
        RightBtnDOWN = 0x0204,
        RightBtnUP = 0x0205,
        RightBtnDoubleClick = 0x0206,
        MBtnDOWN = 0x0207,
        MBtnUP = 0x0208,
        MBtnDoubleClick = 0x0209,
        MouseWHEEL = 0x020A,
        XBtnDOWN = 0x020B,
        XBtnUP = 0x020C,
        XBtnDoubleClick = 0x020D,
        MouseHWHEEL = 0x020E
    }
}
