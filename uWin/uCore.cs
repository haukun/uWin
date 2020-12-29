using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace uWin
{

    class uCore
    {
        private uJson uJson;
        private uWindow uWindow;

        public uCore()
        {
            uJson = new uJson();
            uWindow = new uWindow();

            uJson.Retrieve();
            uWindow.ProcessStartTrace(Event_Arrived);
           
        }




        //  プロセスイベント発生時
        void Event_Arrived(object sender, System.Management.EventArrivedEventArgs e)
        {
            var risedProcessName = e.NewEvent.Properties["ProcessName"].Value;

            //  発生したプロセス名が、設定Jsonで指定された中に存在するかチェック
            foreach (uWinJson.Setting setting in this.uJson.json.Settings) {
                if (setting.ProcessName is null) continue;
                if (risedProcessName.Equals(setting.ProcessName))
                {
                    //  該当プロセスを取得
                    Process risedProcess = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(risedProcessName.ToString()))[0];
                    IntPtr hWnd = risedProcess.MainWindowHandle;

                    //  該当プロセスのウィンドウが表示されるまで待機
                    while(!uWindow.IsWindowisible(hWnd))
                    {
                        hWnd = risedProcess.MainWindowHandle;   //  何故か取得し直さないとうまくいかなかったので再取得。詳細は未検証。

                        //  TODO:   無限ループするので回数指定して抜けた方がいい
                    }

                    //  該当プロセスのウィンドウを移動する
                    int ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                    int ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                    int WindowWidth, WindowHeight;
                    uWindow.GetWindowSize(hWnd, out WindowWidth, out WindowHeight);

                    int offsetX = 0;
                    
                    if(! (setting.Place is null))
                    {
                        offsetX = setting.Place.ToLower().Equals(uWinJson.Place.Right.ToLower()) ? ScreenWidth / 2 : 0;
                    }

                    uWindow.SetWindowPos(hWnd, (ScreenWidth / 2 - WindowWidth) / 2 + offsetX, (ScreenHeight - WindowHeight) / 2, WindowWidth, WindowHeight);
                }
            }
        }
    }
}