namespace HideClicker
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Core;

    public partial class MainWindow : Window
    {
        public GameModel Model { get; set; }
        private IntPtr _h;



        public MainWindow()
        {
            Model = new GameModel();
            this.DataContext = Model;

            InitializeComponent();

            ResetHandler_Click(null, null);
        }

        private GameCore _farmGc;
        private GameCore GetFarmGameCore()
        {
            if (_farmGc != null)
                return _farmGc;

            _farmGc = GameCore.GetNew("Main");

            var g = _farmGc.RootGroupAction
                .AddGroup("Farm")
                    .AddSimple("Open Farm", MonsGame.Menu.Fight.LClick)
                    .AddPeriodical("Farm Click", MonsGame.Fight.Mons.LClick, TimeSpan.FromSeconds(30), TimeSpan.FromMilliseconds(10))
                    .End()
                .AddGroup("Upgrade")
                    .AddSimple("Open Avatars", MonsGame.Menu.Avatars.LClick)
                    .AddSimple("Upgrade", () =>
                    {
                        if (Model.IsDpsMode)
                        {
                            if (!Model.IsDpcOnly)
                            {
                                MonsGame.Avatars.A24.LClick();
                                Thread.Sleep(500);
                                MonsGame.Avatars.Buy.LClick(5);
                                Thread.Sleep(1000);
                            }
                            if (!Model.IsDpsOnly)
                            {
                                MonsGame.Avatars.A1.LClick();
                                Thread.Sleep(500);
                                MonsGame.Avatars.Buy.LClick(5);
                            }
                        }
                        else
                        {
                            if (!Model.IsDpsOnly)
                            {
                                MonsGame.Avatars.A1.LClick();
                                Thread.Sleep(500);
                                MonsGame.Avatars.Buy.LClick(5);
                                Thread.Sleep(1000);
                            }
                            if (!Model.IsDpcOnly)
                            {
                                MonsGame.Avatars.A24.LClick();
                                Thread.Sleep(500);
                                MonsGame.Avatars.Buy.LClick(5);
                            }
                        }
                    })
                    .End();

            return _farmGc;
        }

        private GameCore _saveGC;
        private GameCore GetSaveGameCore()
        {
            if (_saveGC != null)
                return _saveGC;

            _saveGC = GameCore.GetNew("Reset");

            var g = _saveGC.RootGroupAction
                .AddSimple("Open Save/Settings", MonsGame.Menu.SaveSettings.LClick)
                .AddWait(1)
                .AddSimple("Click Save", MonsGame.SaveSettings.Save.LClick)
                .AddWait(2)
                .AddSimple("Open fight", MonsGame.Menu.Fight.LClick)
                .AddWait(1)
                .AddSimple("Boosts", () =>
                {
                    if (!Model.UseBoosts) return;
                    MonsGame.Fight.Sword.LClick();
                    MonsGame.Fight.Sharingan.LClick();
                    MonsGame.Fight.Glove.LClick();
                    MonsGame.Fight.Boss.LClick();

                })
                .End();

            return _saveGC;
        }

        private GameCore _resetGC;
        private GameCore GetResetGameCore()
        {
            if (_resetGC != null)
                return _resetGC;

            _resetGC = GameCore.GetNew("Reset");

            var g = _resetGC.RootGroupAction
                .AddSimple("Open Artifacts", MonsGame.Menu.Artifacts.LClick)
                .AddWait(1)
                .AddSimple("Click reset", MonsGame.Artifacts.Reset_Yes.LClick)
                .AddWait(1)
                .AddSimple("Click Yes", MonsGame.Artifacts.Reset_Yes.LClick)
                .AddWait(2)
                .End();

            return _resetGC;
        }

        private CancellationTokenSource _ts;
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _ts = new CancellationTokenSource();
            Task.Run(Do);
            Model.IsStarted = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _ts.Cancel();
            Model.IsStarted = false;
        }

        private void Do()
        {
            var farmGc = GetFarmGameCore();
            farmGc.CTS = _ts;

            var saveGc = GetSaveGameCore();
            saveGc.CTS = _ts;

            var resetGc = GetResetGameCore();
            resetGc.CTS = _ts;

            //resetGc.Execute();
            //return;

            var fullTimeout = TimeSpan.FromHours(4) + TimeSpan.FromMinutes(10);
            //fullTimeout = TimeSpan.FromSeconds(20);
            var saveTimeout = TimeSpan.FromMinutes(31);
            //saveTimeout = TimeSpan.FromSeconds(10);

            while (!_ts.IsCancellationRequested)
            {
                //Full circle
                var startTime = DateTime.UtcNow;
                while (!_ts.IsCancellationRequested
                    && DateTime.UtcNow - startTime < fullTimeout)
                {
                    //Save circle
                    var saveTime = DateTime.UtcNow;
                    var startBossFightCounter = 20;
                    while (!_ts.IsCancellationRequested
                        && DateTime.UtcNow - saveTime < saveTimeout
                        && DateTime.UtcNow - startTime < fullTimeout)
                    {
                        Model.BossFightCounter = --startBossFightCounter;
                        Model.SaveTime = saveTimeout - (DateTime.UtcNow - saveTime);
                        Model.ResetTime = fullTimeout - (DateTime.UtcNow - startTime);

                        if (startBossFightCounter == 0)
                        {
                            startBossFightCounter = 20;
                            MonsGame.Menu.Fight.LClick();
                            MonsGame.Fight.Boss.LClick();
                        }

                        farmGc.Execute();

                        Thread.Sleep(1000);
                    }
                    saveGc.Execute();
                }

                resetGc.Execute();
            }
        }

        private void ResetHandler_Click(object sender, RoutedEventArgs e)
        {
            //var h = WinApi.FindWindow(IntPtr.Zero, "Idle Mons - Play on Armor Games - Google Chrome");
            //_h = WinApi.FindWindowEx(h, IntPtr.Zero, "Chrome_RenderWidgetHostHWND", IntPtr.Zero);

            _h = WinApi.FindWindow(IntPtr.Zero, "Idle Mons - Play on Armor Games - Mozilla Firefox");
            KPointExt.WindowsHandle = _h;
        }
    }
}
