using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace uWin
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private uCore uCore;
        //  タスクトレイに表示するアイコン
        private uNotifyIconWrapper uNotifyIcon;

        //  開始イベント
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            uCore = new uCore();
            uNotifyIcon = new uNotifyIconWrapper();
        }

        //  終了イベント
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            uNotifyIcon.Dispose();
        }
    }
}
