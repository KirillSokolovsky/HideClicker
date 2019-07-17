namespace HideClicker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MouseService
    {
        public static void LeftClick(IntPtr windowHandle, int x, int y)
        {
            Action(windowHandle, MouserAction.LeftBtnDOWN, x, y);
            Action(windowHandle, MouserAction.LeftBtnUP, x, y);
        }
        public static void RightClick(IntPtr windowHandle, int x, int y)
        {
            Action(windowHandle, MouserAction.RightBtnDOWN, x, y);
            Action(windowHandle, MouserAction.RightBtnUP, x, y);
        }

        private static void Action(IntPtr windowHandle, MouserAction actionType, int x, int y)
        {
            WinApi.SendMessage(windowHandle, (int)actionType, (IntPtr)0, (IntPtr)(x | (y << 16)));
        }
    }
}
