namespace HideClicker
{
    using HideClicker.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class KPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public KPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal static KPoint N(int x, int y)
        {
            return new KPoint(x, y);
        }
    }

    public static class KPointExt
    {
        public static IntPtr WindowsHandle { get; set; }

        public static void LClick(this KPoint point)
        {
            MouseService.LeftClick(WindowsHandle, point.X, point.Y);
        }
        public static void LClick(this KPoint point, int times)
        {
            for (int i = 0; i < times; i++)
            {
                MouseService.LeftClick(WindowsHandle, point.X, point.Y);
                Thread.Sleep(100);
            }
        }
    }
}
