namespace HideClicker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class MonsGame
    {
        public static class Menu
        {
            public static KPoint Fight { get; } = KPoint.N(520, 310);
            public static KPoint Avatars { get; } = KPoint.N(630, 310);
            public static KPoint Monster { get; } = KPoint.N(720, 310);
            public static KPoint Skills { get; } = KPoint.N(820, 310);
            public static KPoint Artifacts { get; } = KPoint.N(920, 310);
            public static KPoint Rewards { get; } = KPoint.N(1020, 310);
            public static KPoint Shop { get; } = KPoint.N(1120, 310);
            public static KPoint Help { get; } = KPoint.N(1220, 310);
            public static KPoint SaveSettings { get; } = KPoint.N(1320, 310);
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
            public static KPoint A24 { get; } = KPoint.N(1300, 730);

            public static List<KPoint> All { get; } = new List<KPoint>
            {
                A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12
            };
        }

        public static class Fight
        {
            public static KPoint Mons { get; } = KPoint.N(940, 530);
            public static KPoint Boss { get; } = KPoint.N(1231, 411);
            public static KPoint Sword { get; } = KPoint.N(706, 403);
            public static KPoint Sharingan { get; } = KPoint.N(753, 403);
            public static KPoint Glove { get; } = KPoint.N(800, 403);
        }

        public static class Artifacts
        {
            public static KPoint Reset_Yes { get; } = KPoint.N(1290, 750);
        }
        public static class SaveSettings
        {
            public static KPoint Save { get; } = KPoint.N(1065, 450);
        }
    }
}
