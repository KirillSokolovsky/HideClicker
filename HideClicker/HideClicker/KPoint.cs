namespace HideClicker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
}
