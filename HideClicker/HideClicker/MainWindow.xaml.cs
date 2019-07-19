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
        private IntPtr _h;

        public MainWindow()
        {
            InitializeComponent();

            //var h = WinApi.FindWindow(IntPtr.Zero, "Idle Mons - Play on Armor Games - Google Chrome");
            //_h = WinApi.FindWindowEx(h, IntPtr.Zero, "Chrome_RenderWidgetHostHWND", IntPtr.Zero);

            _h = WinApi.FindWindow(IntPtr.Zero, "Idle Mons - Play on Armor Games - Mozilla Firefox");
            KPointExt.WindowsHandle = _h;
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
                    .AddSimple("Click Boss", MonsGame.Fight.Boss.LClick)
                    .End()
                .AddGroup("Upgrade")
                    .AddSimple("Open Avatars", MonsGame.Menu.Avatars.LClick)
                    .AddSimple("Upgrade", () =>
                    {
                        MonsGame.Avatars.A1.LClick();
                        MonsGame.Avatars.Buy.LClick(5);
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
                .AddWait(5)
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
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _ts.Cancel();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Do()
        {
            var farmGc = GetFarmGameCore();
            farmGc.CTS = _ts;

            var saveGc = GetSaveGameCore();
            saveGc.CTS = _ts;

            var resetGc = GetResetGameCore();
            resetGc.CTS = _ts;

            var fullTimeout = TimeSpan.FromHours(3);
            //fullTimeout = TimeSpan.FromSeconds(20);
            var saveTimeout = TimeSpan.FromMinutes(24);
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
                    while(!_ts.IsCancellationRequested
                        && DateTime.UtcNow - saveTime < saveTimeout)
                    {
                        farmGc.Execute();
                        Thread.Sleep(1000);
                    }
                    saveGc.Execute();
                }

                resetGc.Execute();
            }
        }
    }
}
