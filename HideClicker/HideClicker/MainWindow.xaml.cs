namespace HideClicker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MouseService.RightClick(_h, 5, 5);
            MouseService.LeftClick(_h, 530, 450);
        }
    }
}
