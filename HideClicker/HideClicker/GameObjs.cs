namespace HideClicker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class GameObjs
    {
        public static class Menu
        {
            public static KPoint Fight { get; } = KPoint.N(520, 310);
            public static KPoint Avatars { get; } = KPoint.N(630, 310);
            public static KPoint Monster { get; } = KPoint.N(720, 310);
            public static KPoint Skills { get; } = KPoint.N(820, 310);
            public static KPoint Artifacts { get; } = KPoint.N(920, 310);
        }

        public static class Avatars
        {
            public static KPoint Buy { get; } = KPoint.N(910, 750);

            public static KPoint A1 { get; } = KPoint.N(530, 450);
            public static KPoint A2 { get; } = KPoint.N(630, 450);
            public static KPoint A3 { get; } = KPoint.N(730, 450);


            public static KPoint A4 { get; } = KPoint.N(530, 550);
            public static KPoint A5 { get; } = KPoint.N(630, 550);
            public static KPoint A6 { get; } = KPoint.N(730, 550);


            public static KPoint A7 { get; } = KPoint.N(530, 640);
            public static KPoint A8 { get; } = KPoint.N(630, 640);
            public static KPoint A9 { get; } = KPoint.N(730, 640);


            public static KPoint A10 { get; } = KPoint.N(530, 730);
            public static KPoint A11 { get; } = KPoint.N(630, 730);
            public static KPoint A12 { get; } = KPoint.N(730, 730);

            public static List<KPoint> All { get; } = new List<KPoint>
            {
                A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12
            };
        }
    }
}
